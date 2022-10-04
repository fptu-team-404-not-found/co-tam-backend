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
    }
}
