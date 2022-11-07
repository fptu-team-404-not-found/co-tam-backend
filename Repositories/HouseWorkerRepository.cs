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
    public class HouseWorkerRepository : IHouseWorkerRepository
    {
        private readonly cotamContext _dbContext;

        public HouseWorkerRepository(cotamContext context)
        {
            _dbContext = context;
        }

        public List<HouseWorker> GetAllHouseWorkerWithPagination(int page, int pageSize)
        {
            try
            {
                var list = _dbContext.HouseWorkers.Include(x => x.WorkerTags)
                        .Skip((page - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public int CountHouseWorker()
        {
            try
            {
                int count = _dbContext.HouseWorkers.Count();
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public HouseWorker GetHouseWorkerById(int id)
        {
            try
            {
                var houseWorker = _dbContext.HouseWorkers.FirstOrDefault(x => x.Id == id);
                if (houseWorker != null)
                {
                    return houseWorker;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DisableOrEnableHouseWorkerAccount(int id)
        {
            try
            {
                var account = _dbContext.HouseWorkers.FirstOrDefault(x => x.Id == id);
                if (account != null)
                {
                    if (account.Active == 1)
                    {
                        account.Active = 0;
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else if (account.Active == 0)
                    {
                        account.Active = 1;
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else if (account.Active == 2)
                    {
                        account.Active = 1;
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

        public void CreateNewHouseWorker(HouseWorker houseWorker)
        {
            try
            {
                houseWorker.ManagerId = 1;
                _dbContext.HouseWorkers.Add(houseWorker);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateHouseWorker(HouseWorker houseWorker)
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
