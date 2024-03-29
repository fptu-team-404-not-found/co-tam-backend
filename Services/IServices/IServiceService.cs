﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IServiceService
    {
        Task<Response<Service>> GetReponseServiceById(string id);
        Task<Response<List<Service>>> GetReponseServices(int page, int pageSize);
        Task<Response<Service>> GetResponseCreateAService(Service service);
        Task<Response<Service>> GetReponseUpdateService(string id, Service service);
        Task<Response<Service>> GetReponseDeleteService(string id);
    }
}
