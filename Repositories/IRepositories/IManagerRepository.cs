﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IManagerRepository
    {
        List<AdminManager> GetAllManagerWithPagination(int pageIndex, int pageSize);
        bool DisableOrEnableManager(int managerId);
        AdminManager GetManager(int managerId);
        void CreateNewManager(AdminManager manager);
        int CountManager();
        void UpdateManager(AdminManager manager);
        List<AdminManager> SearchAccount(string searchString, int page, int pageSize);
        int CountManagerWhenSearch(string searchString);
    }
}
