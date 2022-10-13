using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IPackageRepository
    {
        List<Package> GetAllPackageByEachServiceWithPagination(int serviceId, int page, int pageSize);
        int CountPackageByServiceId(int serviceId);
        Package GetPackageById(int id);
        bool DisableOrEnablePackage(int packageId);
        void CreateNewPackage(Package package);
        void UpdatePackage(Package package);
    }
}
