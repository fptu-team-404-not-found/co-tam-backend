using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IWorkerTagRepository
    {
        List<WorkerTag> GetAllWorkerTagWithPagination(int pageIndex, int pageSize);
        int CountWorkerTag();
        WorkerTag GetWorkerTagById(int id);
        void CreateNewWorkerTag(WorkerTag workerTag);
        void UpdateWorkerTag(WorkerTag workerTag);
        void RemoveWorkerTag(int id);
        List<WorkerTag> GetWorkerTagsByHouseworkerId(int houseworkerId);
        WorkerTag CheckWorkerTagHasExist(int houseworkerId, string tagName);
        List<WorkerTag> GetWorkerTag();
    }
}
