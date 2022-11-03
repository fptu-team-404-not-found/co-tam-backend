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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IExtraServiceRepository _extraServiceRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository, IExtraServiceRepository extraServiceRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
            _extraServiceRepository = extraServiceRepository;
        }

        public async Task<Response<OrderDetail>> GetOrderDetailById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<OrderDetail>
                    {
                        Message = "Hãy Nhập id Có Giá Trị Lớn Hơn 0",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var res = _orderDetailRepository.GetOrderDetailById(id);
                if (res == null)
                {
                    return new Response<OrderDetail>
                    {
                        Message = "Không tìm thấy order detail với id " + id,
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<OrderDetail> 
                {
                    Data = res,
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

        public async Task<Response<List<OrderDetail>>> GetOrderDetailsByOrderId(int orderId)
        {
            try
            {
                if (orderId <= 0)
                { 
                    return new Response<List<OrderDetail>> { 
                        Message = "Hãy Nhập OrderId Có Giá Trị Lớn Hơn 0",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExistOrder = _orderRepository.GetOrderById(orderId);
                if (checkExistOrder == null)
                {
                    return new Response<List<OrderDetail>>
                    {
                        Message = "Không tìm thấy OrderId",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _orderDetailRepository.GetOrderDetailsByOrderId(orderId);
                if (lst == null)
                {
                    return new Response<List<OrderDetail>>
                    {
                        Message = "Không tìm thấy orderdetail của order này",
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<List<OrderDetail>>
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
