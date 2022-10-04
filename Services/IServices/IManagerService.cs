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
        Task<Response<List<AdminManager>>> GetAllManagerWithPagination(int page);
    }
}
