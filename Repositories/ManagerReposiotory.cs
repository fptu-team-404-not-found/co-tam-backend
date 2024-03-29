﻿using BusinessObject.Models;
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

        public int CountManager()
        {
            var count = _dbContext.AdminManagers.Where(x => x.RoleId == 2).Count();
            return count;
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

        public void UpdateManager(AdminManager manager)
        {
            _dbContext.SaveChanges();
        }
        public List<AdminManager> SearchAccount(string searchString, int page, int pageSize)
        {
            try
            {
                var list = _dbContext.AdminManagers
                        .Where(x => x.RoleId == 2 && x.Name.Contains(searchString) 
                        || x.RoleId == 2 && x.Phone.Contains(searchString) || x.RoleId == 2 && x.Email.Contains(searchString)).Skip((page - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
                if (list != null)
                {
                    return list;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public int CountManagerWhenSearch(string searchString)
        {
            var count = _dbContext.AdminManagers
                        .Where(x => x.RoleId == 2 && x.Name.Contains(searchString)
                        || x.RoleId == 2 && x.Phone.Contains(searchString) || x.RoleId == 2 && x.Email.Contains(searchString)).Count();
            return count;
        }
    }
}
