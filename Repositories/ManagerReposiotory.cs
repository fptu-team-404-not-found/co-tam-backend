using BusinessObject.Models;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ManagerReposiotory : IManagerRepository
    {
        private readonly cotamContext _dbContext;

        public ManagerReposiotory(cotamContext context)
        {
            _dbContext = context;
        }

        public void CreateNewManager(AdminManager manager)
        {
            _dbContext.AdminManagers.Add(manager);
            _dbContext.SaveChanges();
        }

        public bool DisableOrEnableManager(int managerId)
        {
            var manager = _dbContext.AdminManagers.FirstOrDefault(x => x.Id == managerId && x.RoleId == 2);
            if (manager != null)
            {
                if (manager.Active == true)
                {
                    manager.Active = false;
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    manager.Active = true;
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public List<AdminManager> GetAllManager()
        {
            var list = _dbContext.AdminManagers.Where(x => x.RoleId == 2).ToList();
            return list;
        }

        public List<AdminManager> GetAllManagerWithPagination(int pageIndex, int pageSize)
        {
            /*var pageCount = Math.Ceiling(_dbContext.AdminManagers.Count() / pageResults);*/
            var list = _dbContext.AdminManagers
                        .Where(x => x.RoleId == 2).Skip((pageIndex - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
            return list;
        }

        public AdminManager GetManager(int managerId)
        {
            var manager = _dbContext.AdminManagers.FirstOrDefault(x => x.Id == managerId && x.RoleId == 2);
            return manager;
        }
    }
}
