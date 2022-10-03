using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.IRepositories;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AuthHouseworkerRepository : IAuthHouseworkerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly cotamContext _dbContext;

        public AuthHouseworkerRepository(IConfiguration configuration, cotamContext cotamContext)
        {
            _configuration = configuration;
            _dbContext = cotamContext;
        }
        private TokenModel CreateTokenWithHouseworker(HouseWorker houseWorker)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, houseWorker.Id.ToString()),
                new Claim("_id", houseWorker.Id.ToString()),
                new Claim(ClaimTypes.Email, houseWorker.Email),
                new Claim(ClaimTypes.Name, houseWorker.Name.ToString()),
                new Claim(ClaimTypes.Role, "Houseworker"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddSeconds(30),
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
                UserId = houseWorker.Id,
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

        public async Task<ServiceResponse<string>> LoginWithHouseworker(string email, string name)
        {
            var houseworker = await _dbContext.HouseWorkers.FirstOrDefaultAsync(x => x.Email == email);
            if (houseworker != null)
            {
                return new ServiceResponse<string>
                {
                    Data = CreateTokenWithHouseworker(houseworker),
                    Message = "Login with houseworker account successfully",
                    Success = true
                };
            }
            else
            {
                return new ServiceResponse<string>
                {
                    Message = "You don't have permission for this app",
                    Success = true
                };

            }
        }
        public async Task<ServiceResponse<string>> Logout(int userId)
        {
            var refresh = await _dbContext.RefreshTokens.FirstOrDefaultAsync(p => p.UserId == userId);
            if (refresh != null)
            {

                _dbContext.RefreshTokens.Remove(refresh);
                await _dbContext.SaveChangesAsync();
                return new ServiceResponse<string>
                {
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
