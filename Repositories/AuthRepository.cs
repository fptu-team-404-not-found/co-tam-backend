using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.ValidationHandling;
using ServiceResponse;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;

namespace Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly cotamContext _dbContext;
        private readonly ValidationAdminManager _validationAdminManager;

        public AuthRepository(IConfiguration configuration, cotamContext cotamContext, ValidationAdminManager validationAdminManager)
        {
            _configuration = configuration;
            _dbContext = cotamContext;
            _validationAdminManager = validationAdminManager;
        }

        public async Task<ServiceResponse<string>> RenewToken(TokenModel model)
        {
            var response = new ServiceResponse<string>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var tokenValidateParam = new TokenValidationParameters
            {
                //ký vào token 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                //tự cấp token 
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false //ko kiem tra token het han
            };
            try
            {
                //check 1 : AccessToken Valid format
                var tokenInVerification = tokenHandler.ValidateToken(model.AccessToken, tokenValidateParam, out var validatedToken);
                //check 2: check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        response.Success = false;
                        response.Message = "Invalid token";
                        return response;
                    }
                }
                //check 3: check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(
                    x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    response.Success = false;
                    response.Message = "Access token has not yet expired";
                    return response;
                }
                //check 4: check refreshtoken exist in DB
                var storedToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token.Equals(model.RefreshToken));
                if (storedToken == null)
                {
                    response.Success = false;
                    response.Message = "Refresh token does not exist";
                    return response;
                }
                //check 5 : check refresh token is used/revoked?
                if ((bool)storedToken.IsUsed)
                {
                    response.Success = false;
                    response.Message = "Refresh token has been used";
                    return response;
                }
                if ((bool) storedToken.IsRevoked)
                {
                    response.Success = false;
                    response.Message = "Refresh token has been revoked";
                    return response;
                }

                //check 6: AccessToken id == JwtId in RefreshToke
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    response.Success = false;
                    response.Message = "Token doesn't match";
                    return response;
                }

                //Update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;

                var tokenNeedToReplace = await _dbContext.RefreshTokens.FirstOrDefaultAsync(n => n.Token == model.RefreshToken);
                tokenNeedToReplace.IsRevoked = storedToken.IsRevoked;
                tokenNeedToReplace.IsUsed = storedToken.IsUsed;
                await _dbContext.SaveChangesAsync();
                //create new token 
                var user = _dbContext.AdminManagers.FirstOrDefault(n => n.Id == storedToken.UserId);
                var token = CreateTokenWithAdmin(user);

                response.Success = true;
                response.Message = "Renew token success";
                response.Data = token;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Something went wrong";
                return response;
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;
        }

        private TokenModel CreateTokenWithAdmin(AdminManager adminManager)
        {
            var role = _dbContext.Roles.FirstOrDefault(x => x.Id == adminManager.RoleId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, adminManager.Id.ToString()),
                new Claim("_id", adminManager.Id.ToString()),
                new Claim(ClaimTypes.Email, adminManager.Email),
                new Claim(ClaimTypes.Name, adminManager.Name),
                new Claim(ClaimTypes.Role, role.Name.Trim()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = tokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenEntity = new RefreshToken
            {
                RtokenId = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                UserId = adminManager.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.Now,
                ExpiredAt = DateTime.Now
            };
            _dbContext.RefreshTokens.Add(refreshTokenEntity);
            _dbContext.SaveChanges();
            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public async Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name)
        {
            
            var accountAdminManager = await _dbContext.AdminManagers.FirstOrDefaultAsync(x => x.Email == email);
            if (accountAdminManager != null && accountAdminManager.RoleId == 1)
            {
                return new ServiceResponse<string>
                {
                    Data = CreateTokenWithAdmin(accountAdminManager),
                    Success = true,
                    Message = "Login successfully with admin account"
                };
            }
            else if (accountAdminManager != null && accountAdminManager.RoleId == 2)
            {
                return new ServiceResponse<string>
                {
                    Data = CreateTokenWithAdmin(accountAdminManager),
                    Success = true,
                    Message = "Login successfully with manager account"
                };
            }
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Login failed"
            };
        }
        public async Task<ServiceResponse<AdminManager>> GetAdminManager(int id)
        {

            var ad = await _dbContext.AdminManagers.FirstOrDefaultAsync(x => x.Id == id);
            return new ServiceResponse<AdminManager>
            {
                Data = ad,
                Message = "Successfully",
                Success = true
                
            };
        }
        public async Task<ServiceResponse<string>> Logout(int userId)
        {
            var refresh = await _dbContext.RefreshTokens.FirstOrDefaultAsync(p => p.UserId == userId);
            if (refresh != null)
            {
                
                _dbContext.RefreshTokens.Remove(refresh);
                await _dbContext.SaveChangesAsync();
                return new ServiceResponse<string> {
                    Success = true,
                    Message = "Success"
                };
            }
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Failed"
            };
        }

    }
}