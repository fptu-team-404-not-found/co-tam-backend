﻿using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IAuthCustomerRepository
    {
        Task<ServiceResponse<string>> LoginWithCustomer(string email, string name);
        Task<ServiceResponse<string>> Logout(int userId);
        Task<ServiceResponse<string>> LoginWithCustomerVer2(string email, string name);
    }
}
