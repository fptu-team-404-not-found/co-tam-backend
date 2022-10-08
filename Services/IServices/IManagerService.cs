using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IManagerService
    {
        Task<Response<List<AdminManager>>> GetAllManagerWithPagination(int pageIndex, int pageSize);
        Task<Response<string>> DisableOrEnableManager(int managerId);
        Task<Response<AdminManager>> GetManager(int managerId);
        Task<Response<string>> CreateNewManager(AdminManager manager);
        Task<Response<string>> CountManager();
    }
}
