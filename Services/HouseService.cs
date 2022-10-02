using BusinessObject.Models;
using Repositories;
using ServiceResponse;
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

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }
        public async Task<Response<House>> GetHouseById(int id)
        {
            House house = _houseRepository.GetHouseById(id);

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
    }
}
