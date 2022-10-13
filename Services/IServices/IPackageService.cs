using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IPackageService
    {
        Task<Response<int>> CountPackageByServiceId(int serviceId);
        Task<Response<List<Package>>> GetAllPackageByEachServiceWithPagination(int serviceId, int page, int pageSize);
        Task<Response<Package>> GetPackageById(int id);
        Task<Response<string>> DisableOrEnablePackage(int packageId);
        Task<Response<string>> CreateNewPackage(Package package);
        Task<Response<Package>> UpdatePackage(Package package);
    }
}
