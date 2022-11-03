using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IWorkerInOrderService
    {
        Task<Response<List<WorkerInOrder>>> GetAllWorkInOrderWithPagination(int pageIndex, int pageSize);
        Task<Response<int>> CountWorkerInOrder();
        Task<Response<int>> CreateNewWorkInOder(int orderId, int houseworkerId, WorkerInOrder workerInOrder);
        Task<Response<WorkerInOrder>> GetWorkerInOrderById(int id);
        Task<Response<string>> RemoveWorkInOrder(int id);
        Task<Response<string>> UpdateRating(int id, int rating);
        Task<Response<List<WorkerInOrder>>> GetWorkerInOrdersByHouseworkerId(int houseworkerId);
    }
}
