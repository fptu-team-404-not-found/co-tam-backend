using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IAdminRepository
    {
        AdminManager GetAdmin_ManagerById(int id);
        List<AdminManager> GetAllAdminWithPagination(int page, int pageSize);
        bool DisableOrEnableAdmin(int adminId);
        int CountAdmin();
        void CreateNewAdmin(AdminManager admin);
    }
}
