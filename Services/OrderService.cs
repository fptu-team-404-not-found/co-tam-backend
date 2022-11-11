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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHouseRepository _houseRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IHouseRepository houseRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _houseRepository = houseRepository;
        }

        public async Task<Response<string>> ChangeTheOrderState(int orderId)
        {
            try
            {
                var checkExist = _orderRepository.GetOrderById(orderId);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy Order",
                        Success = false,
                        StatusCode = 400
                    };
                }
                OrderStates cancel = OrderStates.DON_BI_HUY;
                if (checkExist.OrderState == Array.IndexOf(Enum.GetValues(cancel.GetType()), cancel))
                {
                    return new Response<string>
                    {
                        Message = "Đơn đã bị hủy không thể đổi trạng thái",
                        Success = true,
                        StatusCode = 400
                    };
                }
                _orderRepository.ChangeTheOrderState(orderId);
                return new Response<string>
                {
                    Message = "Đã đổi trạng thái thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> ChangeTheOrderStateToCancle(int orderId)
        {
            try
            {
                var checkExist = _orderRepository.GetOrderById(orderId);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy Order",
                        Success = false,
                        StatusCode = 400
                    };
                }
                OrderStates cancel = OrderStates.DON_BI_HUY;
                if (checkExist.OrderState == Array.IndexOf(Enum.GetValues(cancel.GetType()), cancel))
                {
                    return new Response<string>
                    {
                        Message = "Đơn đã bị hủy không thể đổi trạng thái",
                        Success = true,
                        StatusCode = 400
                    };
                }
                _orderRepository.ChangeTheOrderStateToCancle(orderId);
                return new Response<string>
                {
                    Message = "Đã hủy đơn thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<int>> CountOrder()
        {
            try
            {
                var count = _orderRepository.CountOrder();
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng order không tồn tại",
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

        public async Task<Response<int>> CountOrdersWhenSearch(string searchString)
        {
            try
            {
                var count = _orderRepository.CountOrdersWhenSearch(searchString);
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng order không tồn tại",
                        Success = false
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

        public async Task<Response<List<Order>>> GetAllOrderWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = _orderRepository.GetAllOrderWithPagination(page, pageSize);
                return new Response<List<Order>>
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

        public async Task<Response<Order>> GetOrderById(int id)
        {
            try
            {
                var order = _orderRepository.GetOrderById(id);
                if (order != null)
                {
                    return new Response<Order>
                    {
                        Data = order,
                        Message = "Thành Công",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new Response<Order>
                    {
                        Message = "Không tìm thấy order có id là " + id,
                        Success = false,
                        StatusCode = 400
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<Order>>> GetOrdersHasStateDangDatByCusId(int cusId)
        {
            try
            {
                if (cusId <= 0)
                {
                    return new Response<List<Order>>
                    {
                        Message = "Hãy Nhập CusId có giá trị lớn hơn 0",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExist = _customerRepository.GetCustomerById(cusId);
                if (checkExist == null)
                {
                    return new Response<List<Order>>
                    {
                        Message = "Không tìm thấy customer với id " + cusId,
                        Success = false,
                        StatusCode = 400
                    };
                }
                var house = _houseRepository.GetHouseByCusId(cusId);
                List<Order> res = new List<Order>();
                foreach (var item in house)
                {
                    res = _orderRepository.GetOrdersHasStateDangDatByCusId(item.Id);
                    if (res == null)
                    {
                        return new Response<List<Order>>
                        {
                            Message = "Không tìm thấy order Dang Dat",
                            Success = false,
                            StatusCode = 200
                        };
                    }
                    return new Response<List<Order>>
                    {
                        Data = res,
                        Message = "Thành Công",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new Response<List<Order>>
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

        public async Task<Response<List<Order>>> GetOrdersHistoryByCusId(int cusId)
        {
            try
            {
                if (cusId <= 0)
                {
                    return new Response<List<Order>>
                    { 
                        Message = "Hãy Nhập CusId có giá trị lớn hơn 0",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExist = _customerRepository.GetCustomerById(cusId);
                if (checkExist == null)
                {
                    return new Response<List<Order>>
                    {
                        Message = "Không tìm thấy customer với id "+cusId,
                        Success = false,
                        StatusCode = 400
                    };
                }
                var house = _houseRepository.GetHouseByCusId(cusId);
                List<Order> res = new List<Order>();
                List<Order> realRes = new List<Order>();
                foreach (var item in house)
                {
                    res = _orderRepository.GetOrdersHistoryByCusId(item.Id);
                    if (res != null)
                    {
                        foreach (var item2 in res)
                        {
                            realRes.Add(item2);
                        }
                    }
                }

                if (realRes.Count == 0)
                {
                    return new Response<List<Order>>
                    {
                        Message = "Không có order history",
                        Success = true,
                        StatusCode = 200
                    };
                }
                return new Response<List<Order>>
                {
                    Data = realRes,
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

        public async Task<Response<List<Order>>> SearchOrder(string searchString, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    return new Response<List<Order>>
                    {
                        Message = "Hãy nhập gì đó để tìm kiếm",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _orderRepository.SearchOrder(searchString, page, pageSize);
                if (lst.Count() == 0)
                {
                    return new Response<List<Order>>
                    {
                        Message = "Không tìm thấy",
                        Success = false,
                        StatusCode = 200
                    };
                }
                return new Response<List<Order>>
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
    }
}
