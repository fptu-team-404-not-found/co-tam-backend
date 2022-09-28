using BusinessObject.Models;
using CoTamApp;
using CoTamApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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

        public AuthRepository(IConfiguration configuration, cotamContext cotamContext)
        {
            _configuration = configuration;
            _dbContext = cotamContext;
        }

        private TokenModel CreateToken(AdminManager adminManager)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, adminManager.Email),
                new Claim(ClaimTypes.Role, adminManager.RoleId.ToString()),
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
            /*var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);*/
            /*var refreshTokenEntity = new RefreshToken
            {
                RTokenId = Guid.NewGuid(),
                JwtId = token.Id,
                UserId = adminManager.Id,
                Token = refreshToken,
                isUsed = false,
                isRevoked = false,
                IssuedAt = DateTime.Now,
                ExpiredAt = DateTime.Now
            };
            _refreshToken.InsertOne(refreshTokenEntity);*/
            return new TokenModel
            {
                AccessToken = accessToken,
                /*RefreshToken = refreshToken*/
            };
        }

        /*private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(1),
                Created = DateTime.Now
            };
            return refreshToken;
        }
        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            *//*HttpContext.Current.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);*/
            /*adminManager.RefreshToken = newRefreshToken.Token;
            adminManager.TokenCreated = newRefreshToken.Created;
            adminManager.TokenExpries = newRefreshToken.Expires;*//*
        }*/

        public async Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name)
        {
            
            var accountAdminManager = await _dbContext.AdminManagers.FirstOrDefaultAsync(x => x.Email == email);
            if (accountAdminManager != null && accountAdminManager.RoleId == 1)
            {
                return new ServiceResponse<string>
                {
                    Data = CreateToken(accountAdminManager),
                    Success = true,
                    Message = "Login successfully with admin account"
                };
            }
            else if (accountAdminManager != null && accountAdminManager.RoleId == 2)
            {
                return new ServiceResponse<string>
                {
                    Data = CreateToken(accountAdminManager),
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
    }
}