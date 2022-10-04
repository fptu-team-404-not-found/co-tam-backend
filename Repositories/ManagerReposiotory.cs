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
        public List<AdminManager> GetAllManagerWithPagination(int page)
        {
            var pageResults = 2f;
            /*var pageCount = Math.Ceiling(_dbContext.AdminManagers.Count() / pageResults);*/
            var list = _dbContext.AdminManagers
                        .Where(x => x.RoleId == 2).Skip((page - 1) * (int)pageResults)
                        .Take((int)pageResults).ToList();
            return list;
        }
    }
}
