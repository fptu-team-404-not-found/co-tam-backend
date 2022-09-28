using CoTamApp.Models;
using Repositories;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService : IAuthService
    {
        public Task<ServiceResponse<AdminManager>> GetAdminManager(int id) => AuthRepository.Instance.GetAdminManager(id);

        public Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name) => AuthRepository.Instance.LoginWithAdminManager(email, name);
    }
}
