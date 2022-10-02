using BusinessObject.Models;
using Repositories.IRepositories;
using ServiceResponse;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public Task<ServiceResponse<AdminManager>> GetAdminManager(int id) => _authRepository.GetAdminManager(id);

        public Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name) => _authRepository.LoginWithAdminManager(email, name);

        public Task<ServiceResponse<string>> Logout(int userId) => _authRepository.Logout(userId);

        public Task<ServiceResponse<string>> RenewToken(TokenModel model) => _authRepository.RenewToken(model);
    }
}
