using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IWorkerInOrderRepository
    {
        List<WorkerInOrder> GetAllWorkInOrderWithPagination(int pageIndex, int pageSize);
        int CountWorkInOrder();
        void CreateNewWorkInOrder(WorkerInOrder workInOrder);
        void RemoveWorkInOrder(int id);
        void UpdateRating(int id, int rating);
        WorkerInOrder GetWorkerInOrderById(int id);
        List<WorkerInOrder> GetWorkerInOrdersByHouseworkerId(int houseworkerId);
    }
}
