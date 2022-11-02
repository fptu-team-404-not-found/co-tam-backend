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
    public class PackageRepository : IPackageRepository
    {
        private readonly cotamContext _dbContext;

        public PackageRepository(cotamContext context)
        {
            _dbContext = context;
        }
        public int CountPackageByServiceId(int serviceId)
        {
            try
            {
                int count = _dbContext.Packages.Count(x => x.ServiceId == serviceId);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateNewPackage(Package package)
        {
            try
            {
                _dbContext.Packages.Add(package);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DisableOrEnablePackage(int packageId)
        {
            try
            {
                var package = _dbContext.Packages.FirstOrDefault(x => x.Id == packageId);
                if (package != null)
                {
                    if (package.Active == 1)
                    {
                        package.Active = 0;
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else if (package.Active == 0)
                    {
                        package.Active = 1;
                        _dbContext.SaveChanges();
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Package> GetAllPackageByEachServiceWithPagination(int serviceId, int page, int pageSize)
        {
            try
            {
                var list = _dbContext.Packages.Include(x => x.Service).Where(x => x.ServiceId == serviceId)
                       .Skip((page - 1) * (int)pageSize)
                       .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Package GetPackageById(int id)
        {
            try
            {
                var package = _dbContext.Packages.Include(x => x.Service).FirstOrDefault(x => x.Id == id);
                if (package != null)
                { 
                    return package;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePackage(Package package)
        {
            try 
            {
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Package GetServiceFromPackegeId(int packageId)
        {
            try
            {
                var pack = _dbContext.Packages.Include(x => x.Service).FirstOrDefault(x => x.Id == packageId);
                if (pack != null)
                {
                    return pack;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
