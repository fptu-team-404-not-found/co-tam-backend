﻿using Repositories.IRepositories;
using ServiceResponse;
using Services.IServices;
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

        public Task<ServiceResponse<string>> LoginWithHouseworkerVer2(string email) => _authHouseworkerRepository.LoginWithHouseworkerVer2(email);

        public Task<ServiceResponse<string>> Logout(int userId) => _authHouseworkerRepository.Logout(userId);
    }
}
