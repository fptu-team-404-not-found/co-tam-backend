using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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

        public void CreateNewAdmin(AdminManager admin)
        {
            _dbContext.AdminManagers.Add(admin);
            _dbContext.SaveChanges();
        }

        public bool DisableOrEnableAdmin(int adminId)
        {
            var ad = _dbContext.AdminManagers.FirstOrDefault(x => x.Id == adminId && x.RoleId == 1);
            if (ad != null)
            {
                if (ad.Active == true)
                {
                    ad.Active = false;
                    _dbContext.SaveChanges();
                    return true;
                }
                else 
                {
                    ad.Active = true;
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public AdminManager GetAdmin_ManagerById(int id)
        {
            var ad = _dbContext.AdminManagers.FirstOrDefault(x => x.Id == id);
            return ad;
        }

        public int CountAdmin()
        {
            int count = _dbContext.AdminManagers.Where(x => x.RoleId == 1).Count();
            return count;
        }

        public List<AdminManager> GetAllAdminWithPagination(int page, int pageSize)
        {
            /*var pageCount = Math.Ceiling(_dbContext.AdminManagers.Count() / pageResults);*/
            var list = _dbContext.AdminManagers
                        .Where(x => x.RoleId == 1).Skip((page - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
            return list;
        }

        public void UpdateAdmin(AdminManager admin)
        {
            _dbContext.SaveChanges();
            
        }
    }
}
