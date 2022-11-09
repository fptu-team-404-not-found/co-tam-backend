using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IWorkerTagService
    {
        Task<Response<List<WorkerTag>>> GetAllWorkerTagWithPagination(int pageIndex, int pageSize);
        Task<Response<int>> CountWorkerTag();
        Task<Response<WorkerTag>> GetWorkerTagById(int id);
        Task<Response<WorkerTag>> CreateNewWorkerTag(WorkerTag workerTag);
        Task<Response<WorkerTag>> UpdateWorkerTag(WorkerTag workerTag);
        Task<Response<string>> RemoveWorkerTag(int id);
        Task<Response<List<WorkerTag>>> GetWorkerTagsByHouseworkerId(int houseworkerId);
        Task<Response<List<WorkerTag>>> GetWorkerTag();
    }
}
