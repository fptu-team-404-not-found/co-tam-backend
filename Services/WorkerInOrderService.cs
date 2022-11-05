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
        private readonly IHouseWorkerRepository _houseWorkerRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHouseRepository _houseRepository;

        public WorkerInOrderService(IWorkerInOrderRepository workerInOrderRepository, IOrderRepository orderRepository, IHouseWorkerRepository houseWorkerRepository, ICustomerRepository customerRepository, IHouseRepository houseRepository)
        {
            _workerInOrderRepository = workerInOrderRepository;
            _orderRepository = orderRepository;
            _houseWorkerRepository = houseWorkerRepository;
            _customerRepository = customerRepository;
            _houseRepository = houseRepository;
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
                _orderRepository.ChangeTheOrderState(orderId);
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

        public async Task<Response<List<WorkerInOrder>>> GetListWorkInOrderWithoutRatingWithCustomerId(int cusId, int pageIndex, int pageSize)
        {
            try
            {
                if (pageIndex <= 1)
                {
                    pageIndex = 1;
                }
                if (cusId <= 0)
                {
                    return new Response<List<WorkerInOrder>>
                    {
                        Message = "Hãy Nhập CusId có giá trị lớn hơn 0",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExist = _customerRepository.GetCustomerById(cusId);
                if (checkExist == null)
                {
                    return new Response<List<WorkerInOrder>>
                    {
                        Message = "Không tìm thấy customer với id " + cusId,
                        Success = false,
                        StatusCode = 400
                    };
                }
                var house = _houseRepository.GetHouseByCusId(cusId);
                List<Order> res = new List<Order>();
                List<WorkerInOrder> resWio = new List<WorkerInOrder>();
                List<WorkerInOrder> resWio2 = new List<WorkerInOrder>();
                foreach (var item in house)
                {
                    res = _orderRepository.GetOrdersHistoryByCusId(item.Id);
                    if (res == null)
                    {
                        return new Response<List<WorkerInOrder>>
                        {
                            Message = "Không tìm thấy order",
                            Success = false,
                            StatusCode = 400
                        };
                    }
                    foreach (var item2 in res)
                    {
                        resWio = _workerInOrderRepository.GetListWorkInOrderWithoutRatingWithCustomer(item2.Id, pageIndex, pageSize);
                        if (resWio != null)
                        {
                            foreach (var item3 in resWio)
                            {
                                resWio2.Add(item3);
                            }
                        }
                        
                    }
                    if (resWio == null)
                    {
                        return new Response<List<WorkerInOrder>>
                        {
                            Message = "Không tìm thấy work in order",
                            Success = false,
                            StatusCode = 400
                        };
                    }
                    return new Response<List<WorkerInOrder>>
                    {
                        Data = resWio2,
                        Message = "Thành Công",
                        Success = true,
                        StatusCode = 200
                    };
                }

                return new Response<List<WorkerInOrder>>
                {
                    Data = resWio2,
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

        public async Task<Response<List<WorkerInOrder>>> GetWorkerInOrdersByHouseworkerId(int houseworkerId)
        {
            try
            {
                if (houseworkerId <= 0)
                {
                    return new Response<List<WorkerInOrder>>
                    { 
                        Message = "Hãy Nhập HouseworkerId có giá trị lớn hơn 0",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExist = _houseWorkerRepository.GetHouseWorkerById(houseworkerId);
                if (checkExist == null)
                {
                    return new Response<List<WorkerInOrder>>
                    {
                        Message = "Không tìm thấy houseworke với id "+ houseworkerId,
                        Success = false,
                        StatusCode = 400
                    };
                }
                var res = _workerInOrderRepository.GetWorkerInOrdersByHouseworkerId(houseworkerId);
                if (res == null)
                {
                    return new Response<List<WorkerInOrder>>
                    {
                        Message = "Không tìm thấy work in order nào",
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<List<WorkerInOrder>>
                {
                    Data = res,
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

        public async Task<Response<string>> RemoveWorkInOrder(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<string> {
                        Message = "Hãy Nhập Id Với Giá Trị Lớn Hơn 0",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var checkExist = _workerInOrderRepository.GetWorkerInOrderById(id);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy work in order",
                        StatusCode = 400,
                        Success = false
                    };
                }
                _workerInOrderRepository.RemoveWorkInOrder(id);
                return new Response<string>
                {
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

        public async Task<Response<string>> UpdateRating(int id, int rating)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<string>
                    {
                        Message = "Hãy Nhập Id Với Giá Trị Lớn Hơn 0",
                        StatusCode = 400,
                        Success = false
                    };
                }
                if (rating < 0 || rating > 5)
                {
                    return new Response<string>
                    {
                        Message = "Hãy Nhập rating có giá trị từ 0 đến 5 sao",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var checkExist = _workerInOrderRepository.GetWorkerInOrderById(id);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy work in order",
                        StatusCode = 400,
                        Success = false
                    };
                }
                _workerInOrderRepository.UpdateRating(id, rating);
                return new Response<string>
                {
                    Message = "Thành Công",
                    StatusCode = 201,
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
