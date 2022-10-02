using Repositories;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthCustomerService : IAuthCustomerService
    {
        private readonly IAuthCustomerRepository _authCustomerRepository;

        public AuthCustomerService(IAuthCustomerRepository authCustomerRepository)
        {
            _authCustomerRepository = authCustomerRepository;
        }
        public Task<ServiceResponse<string>> LoginWithCustomer(string email, string name) => _authCustomerRepository.LoginWithCustomer(email, name);
    }
}
