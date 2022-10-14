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

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
    }
}
