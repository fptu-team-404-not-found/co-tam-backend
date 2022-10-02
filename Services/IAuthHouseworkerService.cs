using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAuthHouseworkerService
    {
        Task<ServiceResponse<string>> LoginWithHouseworker(string email, string name);
    }
}
