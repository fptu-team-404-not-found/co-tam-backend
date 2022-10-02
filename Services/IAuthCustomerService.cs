using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAuthCustomerService
    {
        Task<ServiceResponse<string>> LoginWithCustomer(string email, string name);
    }
}
