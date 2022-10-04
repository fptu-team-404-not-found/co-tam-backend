using BusinessObject.Models;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly cotamContext _dbContext;

        public AdminRepository(cotamContext cotamContext)
        {
            _dbContext = cotamContext;
        }
        public AdminManager GetAdmin_ManagerById(int id)
        {
            var ad = _dbContext.AdminManagers.FirstOrDefault(x => x.Id == id);
            return ad;
        }


        public List<AdminManager> GetAllAdminWithPagination(int page)
        {
            var pageResults = 2f;
            /*var pageCount = Math.Ceiling(_dbContext.AdminManagers.Count() / pageResults);*/
            var list = _dbContext.AdminManagers
                        .Where(x => x.RoleId == 1).Skip((page - 1) * (int)pageResults)
                        .Take((int)pageResults).ToList();
            return list;
        }
    }
}
