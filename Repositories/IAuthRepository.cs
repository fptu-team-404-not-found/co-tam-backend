using BusinessObject.Models;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name);
        Task<ServiceResponse<AdminManager>> GetAdminManager(int id);
        
    }
}
