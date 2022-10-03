using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAuthCustomerService
    {
        Task<ServiceResponse<string>> LoginWithCustomer(string email, string name);
        Task<ServiceResponse<string>> Logout(int userId);
    }
}
