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
                new Claim("_id", customer.Id.ToString()),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.Name, customer.Name.ToString()),
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
    }
}
