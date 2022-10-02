using Repositories;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthHouseworkerService : IAuthHouseworkerService
    {
        private readonly IAuthHouseworkerRepository _authHouseworkerRepository;

        public AuthHouseworkerService(IAuthHouseworkerRepository authHouseworkerRepository)
        {
            _authHouseworkerRepository = authHouseworkerRepository;
        }
        public Task<ServiceResponse<string>> LoginWithHouseworker(string email, string name) => _authHouseworkerRepository.LoginWithHouseworker(email, name);
    }
}
