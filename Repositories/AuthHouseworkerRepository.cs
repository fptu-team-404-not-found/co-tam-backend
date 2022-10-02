using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

            return new TokenModel
            {
                AccessToken = accessToken,
            };
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
    }
}
