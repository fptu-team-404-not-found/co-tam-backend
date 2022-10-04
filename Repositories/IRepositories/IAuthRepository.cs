using BusinessObject.Models;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name);
        Task<ServiceResponse<string>> RenewToken(TokenModel model);
        Task<ServiceResponse<string>> Logout(int userId);
    }
}
