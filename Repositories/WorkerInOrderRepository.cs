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
    public class WorkerInOrderRepository : IWorkerInOrderRepository
    {
        private readonly cotamContext _dbContext;

        public WorkerInOrderRepository(cotamContext context)
        {
            _dbContext = context;
        }

        public int CountWorkInOrder()
        {
            try
            {
                int count = _dbContext.WorkerInOrders.Count();
                return count;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void CreateNewWorkInOrder(WorkerInOrder workInOrder)
        {
            try
            {
                _dbContext.WorkerInOrders.Add(workInOrder);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<WorkerInOrder> GetAllWorkInOrderWithPagination(int pageIndex, int pageSize)
        {
            try
            {
                var list = _dbContext.WorkerInOrders.Include(x => x.Order).Include(x => x.HouseWorker).Skip((pageIndex - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
        /*public List<HouseWorker> GetHouseWorkersFree()
        {
            try
            {
                var lst = _dbContext.WorkerInOrders
                    .Include(x => x.Order)
                    .Include(x => x.HouseWorker)
                    .Where(x => x.HouseWorker.Active == 1)
                    .ToList();
                var service = _dbContext.Packages.Include(x => x.Service).Where(x => x.Id)
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }*/
        public WorkerInOrder GetWorkerInOrderById(int id)
        {
            try
            {
                var wio = _dbContext.WorkerInOrders.Include(x => x.Order).Include(x => x.HouseWorker).FirstOrDefault(x => x.Id == id);
                if (wio != null)
                {
                    return wio;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void RemoveWorkInOrder(int id)
        {
            try
            {
                var wio = _dbContext.WorkerInOrders.FirstOrDefault(x => x.Id == id);
                if (wio != null)
                {
                    _dbContext.WorkerInOrders.Remove(wio);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void UpdateRating(int id, int rating)
        {
            try
            {
                var wio = _dbContext.WorkerInOrders.FirstOrDefault(x => x.Id == id);
                if (wio != null)
                {
                    wio.Rating = rating;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<WorkerInOrder> GetWorkerInOrdersByHouseworkerId(int houseworkerId)
        {
            OrderStates hoanThanh = OrderStates.CUSTOMER_XAC_NHAN_DON_DA_HOAN_THANH;
            try
            {
                var wio = _dbContext.WorkerInOrders
                    .Include(x => x.Order)
                    .Include(x => x.HouseWorker)
                    .Where(x => x.HouseWorkerId == houseworkerId && x.Order.OrderState == Array.IndexOf(Enum.GetValues(hoanThanh.GetType()), hoanThanh))
                    .ToList();
                if (wio != null)
                {
                    return wio;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<WorkerInOrder> GetListWorkInOrderWithoutRatingWithCustomer(int orderId)
        {
            try
            {
                var order = _dbContext.Orders.FirstOrDefault(x => x.Id == orderId);
                if (order != null)
                { 
                    var wio = _dbContext.WorkerInOrders
                        .Include(x => x.Order)
                        .Include(x => x.HouseWorker)
                        .Where(x => x.OrderId == orderId && x.Rating == null && x.Order.OrderState == 6).ToList();
                    if (wio.Count != 0)
                    {
                        return wio;
                    }
                    return null;
                }   
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public WorkerInOrder GetWorkerInOrderByOrderId(int orderId)
        {
            try
            {
                var wio = _dbContext.WorkerInOrders
                    .Include(x => x.HouseWorker)
                    .Include(x => x.Order)
                    .Include(x => x.Order.OrderDetails)
                    .FirstOrDefault(x => x.OrderId == orderId);
                if (wio != null)
                {
                    return wio;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void UpdateHouseworkerIdToWorkerInOrderById(WorkerInOrder wio)
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
        public WorkerInOrder GetWorkerInOrderByHouseworkerId(int houseworkerId)
        {
            try
            {
                var wio = _dbContext.WorkerInOrders
                    .Include(x => x.Order)
                    .Include(x => x.HouseWorker)
                    .FirstOrDefault(x => x.HouseWorkerId == houseworkerId && x.Order.OrderState == 2);
                if (wio != null)
                {
                    return wio;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void UpdateHouseworkerActiveAgain(int orderId)
        {
            try
            {
                var wio = _dbContext.WorkerInOrders
                    .Include(x => x.HouseWorker)
                    .Include(x => x.Order)
                    .FirstOrDefault(x => x.OrderId == orderId);
                if (wio != null)
                {
                    wio.HouseWorker.Active = 1;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
