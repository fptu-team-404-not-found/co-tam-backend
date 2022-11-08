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
    public class AuthCustomerRepository : IAuthCustomerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly cotamContext _dbContext;

        public AuthCustomerRepository(IConfiguration configuration, cotamContext cotamContext)
        {
            _configuration = configuration;
            _dbContext = cotamContext;
        }
        private TokenModel CreateTokenWithCustomer(Customer customer)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim("id", customer.Id.ToString()),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.Name, customer.Name.ToString()),
                new Claim(ClaimTypes.Role, "Customer"),
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
                UserId = customer.Id,
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

        public async Task<ServiceResponse<string>> LoginWithCustomer(string email, string name)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Email == email);
            if (customer != null)
            {
                return new ServiceResponse<string>
                {
                    Data = CreateTokenWithCustomer(customer),
                    Message = "Login with customer account successfully",
                    Success = true
                };
            }
            else
            {
                var newCus = new Customer();
                newCus.Email = email;
                newCus.Name = name;
                _dbContext.Customers.Add(newCus);
                await _dbContext.SaveChangesAsync();
                return new ServiceResponse<string>
                {
                    Data = CreateTokenWithCustomer(newCus),
                    Message = "Create acount successfully and now login successfully",
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
        public async Task<ServiceResponse<string>> LoginWithCustomerVer2(string email, string name)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Email == email);
            if (customer != null)
            {
                return new ServiceResponse<string>
                {
                    Data = CreateTokenWithCustomer(customer),
                    Message = "Login with customer account successfully",
                    Success = true
                };
            }
            else 
            {
                if (string.IsNullOrEmpty(name))
                {
                    return new ServiceResponse<string>
                    {
                        Message ="Tài khoản của bạn không tồn tại, Bạn Hãy nhập name để tài khoản được tạo tự động tạo mới",
                        Success = false
                    };
                }
                var newCus = new Customer();
                newCus.Email = email;
                newCus.Name = name;
                _dbContext.Customers.Add(newCus);
                await _dbContext.SaveChangesAsync();
                return new ServiceResponse<string>
                {
                    Data = CreateTokenWithCustomer(newCus),
                    Message = "Create acount successfully and now login successfully",
                    Success = true
                };
            }
        }
    }
}
