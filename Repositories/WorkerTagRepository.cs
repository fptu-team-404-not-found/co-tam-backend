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
    public class WorkerTagRepository : IWorkerTagRepository
    {
        private readonly cotamContext _dbContext;

        public WorkerTagRepository(cotamContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int CountWorkerTag()
        {
            try
            {
                int count = _dbContext.WorkerTags.Count();
                return count;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<WorkerTag> GetAllWorkerTagWithPagination(int pageIndex, int pageSize)
        {
            try
            {
                var list = _dbContext.WorkerTags.Include(x => x.HouseWorker).Skip((pageIndex - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public WorkerTag GetWorkerTagById(int id)
        {
            try
            {
                var wt = _dbContext.WorkerTags.Include(x => x.HouseWorker).FirstOrDefault(x => x.Id == id);
                if (wt != null)
                {
                    return wt;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void CreateNewWorkerTag(WorkerTag workerTag)
        {
            try
            {
                _dbContext.WorkerTags.Add(workerTag);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void UpdateWorkerTag(WorkerTag workerTag)
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
        public void RemoveWorkerTag(int id)
        {
            try
            {
                var wt = _dbContext.WorkerTags.FirstOrDefault(x => x.Id == id);
                if (wt != null)
                {
                    _dbContext.WorkerTags.Remove(wt);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<WorkerTag> GetWorkerTagsByHouseworkerId(int houseworkerId)
        {
            try
            {
                var wt = _dbContext.WorkerTags
                    .Include(x => x.HouseWorker)
                    .Where(x => x.HouseWorkerId == houseworkerId)
                    .ToList();
                if (wt != null)
                {
                    return wt;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public WorkerTag CheckWorkerTagHasExist(int houseworkerId, string tagName)
        {
            try
            {
                var wt = _dbContext.WorkerTags.FirstOrDefault(x => x.HouseWorkerId == houseworkerId && x.Name.Equals(tagName));
                if (wt != null)
                {
                    return wt;
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
