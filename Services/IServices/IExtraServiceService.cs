using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IExtraServiceService
    {
        Task<Response<List<ExtraService>>> GetReponseExtraServices();
        Task<Response<ExtraService>> GetReponseExtraServiceById(string id);
        Task<Response<ExtraService>> GetResponseCreateAExtraService(ExtraService extraService);
        Task<Response<ExtraService>> GetReponseDeleteExtraService(string id);
    }
}
