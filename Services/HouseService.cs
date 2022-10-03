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

        public HouseService(IHouseRepository houseRepository, HouseValidation houseValidation, CustomerValidation customerValidation)
        {
            _houseRepository = houseRepository;
            _houseValidation = houseValidation;
            _customerValidation = customerValidation;
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

        public Task<Response<int>> CountHousesByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Response<House>> DeleteHouseForCustomer(House house)
        {
            throw new NotImplementedException();
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

        public async Task<Response<List<House>>> GetHouseListByCustomerId(string customerId)
        {
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

            List<House> houses = _houseRepository.GetListByCustomerId(_customerId);
            return new Response<List<House>>
            {
                Data = houses,
                Message = "Thành công",
                Success = true
            };
        }

        public Task<Response<House>> UpdateHouseForCustomer(House house)
        {
            throw new NotImplementedException();
        }
    }
}
