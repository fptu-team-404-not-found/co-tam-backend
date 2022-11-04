using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAuthHouseworkerService
    {
        Task<ServiceResponse<string>> LoginWithHouseworker(string email, string name);
        Task<ServiceResponse<string>> Logout(int userId);
        Task<ServiceResponse<string>> LoginWithHouseworkerVer2(string email);
    }
}
