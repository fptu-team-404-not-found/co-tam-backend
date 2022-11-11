using BusinessObject.Models;
using Repositories;
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
    public class CustomerPromotionService : ICustomerPromotionService
    {
        private readonly ICustomerPromotionRepository _customerPromotionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly CustomerPromotionValidation _customerPromotionValidation;

        public CustomerPromotionService(ICustomerPromotionRepository customerPromotionRepository, ICustomerRepository customerRepository, CustomerPromotionValidation customerPromotionValidation)
        {
            _customerPromotionRepository = customerPromotionRepository;
            _customerRepository = customerRepository;
            _customerPromotionValidation = customerPromotionValidation;
        }
        public async Task<Response<string>> CountCustomerPromotion(int cusId)
        {
            try
            {
                var checkExist = _customerRepository.GetCustomerById(cusId);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy customer",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var count = _customerPromotionRepository.CountCustomerPromotion(cusId);
                return new Response<string>
                {
                    Data = count.ToString(),
                    Message = "Số lượng khuyến mãi của id: " + cusId +" là "+count.ToString(),
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> CreateNewCustomerPromotion(CustomerPromotion customerPromotion)
        {
            try
            {
                var validate = _customerPromotionValidation.CheckCreateNewCustomerValidation(customerPromotion);
                if (validate != "ok")
                {
                    return new Response<string>
                    {
                        Message = validate,
                        Success = false,
                        StatusCode = 400
                    };
                }
                customerPromotion.IsUsed = false;
                var checkHasPromotion = _customerPromotionRepository.CheckCustomerHasThisPromotion(customerPromotion.CustomerId, customerPromotion.PromotionId);
                if (checkHasPromotion == true)
                {
                    return new Response<string>
                    {
                        Message = "Đã có mã khuyến mãi, không thể tạo mới",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var res = _customerPromotionRepository.CreateNewCustomerPromotion(customerPromotion);
                if (res != "ok")
                {
                    return new Response<string>
                    {
                        Message = res,
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<string>
                {
                    Message = "Thành Công",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Response<string>> DisableCustomerPromotions(int cusId)
        {
            try
            {
                var checkExist = _customerRepository.GetCustomerById(cusId);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy customer",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var res = _customerPromotionRepository.DisableOrEnableCustomerPromotions(cusId);
                if (res == false)
                {
                    return new Response<string>
                    {
                        Message = "Không thể disable customer promotion",
                        StatusCode = 400,
                        Success = false
                    };
                }
                return new Response<string> {
                    Message = "Disable promotion thành công sau khi đã sử dụng",
                    Success = true, 
                    StatusCode = 200
                };
            }
            catch (Exception ex) { 
                throw new Exception (ex.Message);
            }
        }

        public async Task<Response<List<CustomerPromotion>>> GetAllCustomerPromotionWithPagination(int cusId, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var checkExist = _customerRepository.GetCustomerById(cusId);
                if (checkExist == null)
                {
                    return new Response<List<CustomerPromotion>>
                    {
                        Message = "Không tìm thấy customer",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var lst = _customerPromotionRepository.GetAllCustomerPromotionWithPagination(cusId, page, pageSize);
                return new Response<List<CustomerPromotion>>
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

        public async Task<Response<CustomerPromotion>> GetCustomerPromotionById(int cusId)
        {
            try
            {
                var cusPro = _customerPromotionRepository.GetCustomerPromotionById(cusId);
                if(cusPro == null)
                {
                    return new Response<CustomerPromotion>
                    {
                        Message = "Không tìm thấy customer",
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<CustomerPromotion>
                {
                    Data = cusPro,
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

        public async Task<Response<List<CustomerPromotion>>> GetCustomerPromotionsNotUseByCusId(int cusId)
        {
            try
            {
                if (cusId <= 0)
                {
                    return new Response<List<CustomerPromotion>>
                    {
                        Message = "Hãy nhập giá trị customer Id lớn hơn 0",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExist = _customerRepository.GetCustomerById(cusId);
                if (checkExist == null)
                {
                    return new Response<List<CustomerPromotion>>
                    {
                        Message = "Không tìm thấy customer",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _customerPromotionRepository.GetCustomerPromotionsNotUseByCusId(cusId);
                if (lst.Count() == 0)
                {
                    return new Response<List<CustomerPromotion>>
                    {
                        Message = "Danh sách voucher rỗng",
                        Success = false,
                        StatusCode = 200
                    };
                }
                return new Response<List<CustomerPromotion>>
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
