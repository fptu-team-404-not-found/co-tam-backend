using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IInformationService
    {
        Task<Response<List<Information>>> GetAllInformationWithPagination(int page, int pageSize);
        Task<Response<string>> CountInformation();
        Task<Response<Information>> GetInformationById(int id);
        Task<Response<string>> CreateNewInformation(Information infor);
        Task<Response<string>> DisableOrEnableInformation(int inforId);
        Task<Response<Information>> UpdateInformation(Information information);
    }
}
