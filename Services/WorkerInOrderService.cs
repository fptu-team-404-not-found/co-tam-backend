using BusinessObject.Models;
using Repositories.IRepositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WorkerInOrderService : IWorkerInOrderService
    {
        private readonly IWorkerInOrderRepository _workerInOrderRepository;
        private readonly IOrderRepository _orderRepository;

        public WorkerInOrderService(IWorkerInOrderRepository workerInOrderRepository, IOrderRepository orderRepository)
        {
            _workerInOrderRepository = workerInOrderRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Response<int>> CountWorkerInOrder()
        {
            try
            {
                var count = _workerInOrderRepository.CountWorkInOrder();
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng work in order không tồn tại",
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<int>
                {
                    Data = count,
                    Message = "Thành Công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<int>> CreateNewWorkInOder(int orderId, int houseworkerId, WorkerInOrder workerInOrder)
        {
            try
            {
                var checkExist = _orderRepository.GetOrderById(orderId);
                if (checkExist == null)
                {
                    return new Response<int>
                    {
                        Message = "Không tìm thấy order",
                        Success = false,
                        StatusCode = 400
                    };
                }
                if (workerInOrder.Rating < 0 || workerInOrder.Rating > 5)
                {
                    return new Response<int>
                    {
                        Message = "Đánh giá từ 0 đến 5 sao",
                        Success = false,
                        StatusCode = 400
                    };
                }
                workerInOrder.OrderId = checkExist.Id;
                workerInOrder.HouseWorkerId = houseworkerId;
                _workerInOrderRepository.CreateNewWorkInOrder(workerInOrder);
                return new Response<int>
                {
                    Message = "Thành Công",
                    Data = workerInOrder.Id,
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<WorkerInOrder>>> GetAllWorkInOrderWithPagination(int pageIndex, int pageSize)
        {
            try
            {
                if (pageIndex <= 1)
                {
                    pageIndex = 1;
                }
                var lst = _workerInOrderRepository.GetAllWorkInOrderWithPagination(pageIndex, pageSize);
                return new Response<List<WorkerInOrder>>
                {
                    Data = lst,
                    Message = "Thành Công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<WorkerInOrder>> GetWorkerInOrderById(int id)
        {
            try
            {
                if (id == null)
                {
                    return new Response<WorkerInOrder>
                    { 
                        Message = "Id không có giá trị",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var workerInOrder = _workerInOrderRepository.GetWorkerInOrderById(id);
                if (workerInOrder == null)
                {
                    return new Response<WorkerInOrder>
                    {
                        Message = "Không tìm thấy work in order",
                        StatusCode = 400,
                        Success = false
                    };
                }
                return new Response<WorkerInOrder>
                {
                    Data = workerInOrder,
                    Message = "Thành Công",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
