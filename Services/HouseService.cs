using BusinessObject.Models;
using Repositories.IRepositories;
using ServiceResponse;
using Services.IServices;
using Services.ValidationHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;
        private readonly HouseValidation _houseValidation;
        private readonly CustomerValidation _customerValidation;
        private readonly ICustomerRepository _customerRepository;

        public HouseService(IHouseRepository houseRepository, HouseValidation houseValidation, CustomerValidation customerValidation, ICustomerRepository customerRepository)
        {
            _houseRepository = houseRepository;
            _houseValidation = houseValidation;
            _customerValidation = customerValidation;
            _customerRepository = customerRepository;
        }

        public async Task<Response<House>> AddHouseForCustomer(House house)
        {
            bool addHouse = _houseRepository.CreateHouse(house);

            if (addHouse == false)
            {
                return new Response<House>
                {
                    Message = "Tạo nhà thất bại!",
                    Success = false
                };
            }

            return new Response<House>
            {
                Data = house,
                Message = "Thành công",
                Success = true
            };
        }

        public async Task<Response<int>> CountHousesByCustomerId(int customerId)
        {
            try
            {
                var customerExist = _customerRepository.GetCustomerById(customerId);
                if (customerExist == null)
                {
                    return new Response<int>
                    {
                        Message = "Id customer không tồn tại",
                        Success = false,
                        StatusCode = 404
                    };
                }
                var count = _houseRepository.CountHouse(customerId);
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng house của customer không tồn tại",
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

        public async Task<Response<string>> DeleteHouseForCustomer(int houseId)
        {
            try
            {
                if (houseId <= 0)
                {
                    return new Response<string>
                    {
                        Message = "Hãy nhập houseId có giá trị lớn hơn 0",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var checkExist = _houseRepository.GetHouseById(houseId);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy house với id "+houseId,
                        StatusCode = 400,
                        Success = false
                    };
                }
                _houseRepository.DeleteHouse(houseId);
                return new Response<string>
                {
                    Message = "Disable/Enable Thành Công",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<House>> GetHouseById(string id)
        {
            int getId = _houseValidation.ValidationId(id);
            if (getId == -1)
            {
                return new Response<House>
                {
                    Message = "Id sai cú pháp!",
                    Success = false
                };
            }


            House house = _houseRepository.GetHouseById(getId);

            if (house == null)
            {
                return new Response<House>
                {
                    Message = "Nhà không tồn tại!",
                    Success = false
                };
            }

            return new Response<House>
            {
                Data = house,
                Message = "Thành công",
                Success = true
            };
        }

        public async Task<Response<List<House>>> GetHouseListByCustomerId(string customerId, int page, int pageSize)
        {
            if (page <= 1)
            {
                page = 1;
            }
            int _customerId = _customerValidation.ValidationId(customerId);
            if (_customerId == -1)
            {
                return new Response<List<House>>
                {
                    Message = "Id sai cú pháp!",
                    Success = false
                };
            }
            //Check customer co ton tai khong
            if (_houseRepository.CountHouse(_customerId) == 0)
            {
                return new Response<List<House>>
                {
                    Message = "Bạn không có ngôi nhà nào!",
                    Success = true
                };
            }

            List<House> houses = _houseRepository.GetListByCustomerId(_customerId, page, pageSize);
            return new Response<List<House>>
            {
                Data = houses,
                Message = "Thành công",
                Success = true
            };
        }

        public async Task<Response<House>> UpdateHouseForCustomer(House house)
        {
            try
            {
                return new Response<House>
                { 
                    Message = "Thành Công"
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        
    }
}
