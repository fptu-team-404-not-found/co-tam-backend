using BusinessObject.Models;
using Repositories.IRepositories;
using Services.IServices;
using Services.ValidationHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerValidation _customerValidation;
        private readonly IOrderRepository _orderRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly ICustomerPromotionRepository _customerPromotionRepository;
        private readonly IExtraServiceRepository _extraServiceRepository;

        public CustomerService(ICustomerRepository customerRepository, CustomerValidation customerValidation, IOrderRepository orderRepository, IHouseRepository houseRepository, ICustomerPromotionRepository customerPromotionRepository, IExtraServiceRepository extraServiceRepository)
        {
            _customerRepository = customerRepository;
            _customerValidation = customerValidation;
            _orderRepository = orderRepository;
            _houseRepository = houseRepository;
            _customerPromotionRepository = customerPromotionRepository;
            _extraServiceRepository = extraServiceRepository;
        }

        public async Task<Response<int>> CustomerOrder(Order order)
        {
            try
            {
                if (order.Id != 0)
                {
                    return new Response<int>
                    {
                        Message = "Không cần thêm Id khi tạo 1 order mới",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var house = _houseRepository.GetHouseById(order.HouseId);
                var cusInPromotion = _customerPromotionRepository.CheckCustomerHasThisPromotion(house.CustomerId, order.PromotionId);
                if (cusInPromotion)
                {
                    var res = _customerPromotionRepository.CheckUsedForThePromotion(house.CustomerId, order.PromotionId);
                    if (res)
                    {
                        _customerRepository.CustomerOrder(order);
                        return new Response<int>
                        {
                            Message = "Tạo mới order thành công",
                            Success = true,
                            StatusCode = 200
                        };
                    }
                    
                }
                _customerRepository.CustomerOrder(order);
                return new Response<int>
                {
                    Data = order.Id,
                    Message = "Tạo mới order thành công",
                    Success = true,
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<OrderDetail>> CustomerOrderDetail(OrderDetail orderDetail, int orderId)
        {
            try
            {
                if (orderDetail.Id != 0)
                {
                    return new Response<OrderDetail>
                    {
                        Message = "Không cần thêm Id khi tạo 1 orderDetail mới",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExist = _orderRepository.GetOrderById(orderId);
                if (checkExist == null)
                {
                    return new Response<OrderDetail>
                    {
                        Message = "Không tìm thấy order",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExistExtraService = _extraServiceRepository.GetExtraServiceById((int)orderDetail.ExtraServiceId);
                if(checkExistExtraService == null)
                {
                    return new Response<OrderDetail>
                    {
                        Message = "Không tìm thấy extra service",
                        Success = false,
                        StatusCode = 400
                    };
                }
                _customerRepository.CustomerOrderDetail(orderDetail, orderId);
                return new Response<OrderDetail>
                {
                    Message = "Tạo mới order detail thành công",
                    Success = true,
                    StatusCode = 200
                };

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Customer>> GetReponseChangeStatusCustomer(string id)
        {
            try
            {
                int _id = _customerValidation.ValidationId(id);
                if (_id < 0)
                {
                    return new Response<Customer>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Customer customer = _customerRepository.GetCustomerById(_id);
                if (customer == null)
                {
                    return new Response<Customer>
                    {
                        Message = "Khách hàng không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                _customerRepository.ChangeCustomerStatus(customer);

                return new Response<Customer>
                {
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Customer>> GetReponseCustomereById(string id)
        {
            try
            {
                int _id = _customerValidation.ValidationId(id);
                if (_id < 0)
                {
                    return new Response<Customer>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Customer customer = _customerRepository.GetCustomerById(_id);
                if (customer == null)
                {
                    return new Response<Customer>
                    {
                        Message = "Khách hàng không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<Customer>
                {
                    Data = customer,
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<Customer>>> GetReponseCustomers(int pageIndex, int pageSize)
        {
            try
            {
                List<Customer> customers = _customerRepository.GetCustomerList(pageIndex, pageSize);

                if (customers == null)
                {
                    return new Response<List<Customer>>
                    {
                        Message = "Danh sách khách hàng không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<List<Customer>>
                {
                    Data = customers,
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Customer>> GetReponseUpdateCustomer(string id, Customer customer)
        {
            try
            {
                int _id = _customerValidation.ValidationId(id);
                if (_id < 0)
                {
                    return new Response<Customer>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Customer _customer = _customerRepository.GetCustomerById(int.Parse(id));
                if (_customer == null)
                {
                    return new Response<Customer>
                    {
                        Message = "Khách hàng không tồn tại!",
                        Success = false,
                        StatusCode = 404,
                    };
                }

                customer.Id = int.Parse(id);
                _customerRepository.UpdateCustomer(customer);

                return new Response<Customer>
                {
                    Data = customer,
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Customer>> GetResponseCreateACustomer(Customer customer)
        {
            try
            {
                if (customer.Id != 0)
                {
                    return new Response<Customer>
                    {
                        Message = "Không cần thêm Id khi tạo 1 khách hàng mới",
                        Success = false,
                        StatusCode = 400
                    };
                }

                _customerRepository.CreateCustomer(customer);
                return new Response<Customer>
                {
                    Data = customer,
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<int>> GetResponseCustomerNumber()
        {
            try
            {
                int customerNumber = _customerRepository.CountCustomers();
                return new Response<int>
                {
                    Data = customerNumber,
                    Message = "Thành công",
                    StatusCode = 200,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
