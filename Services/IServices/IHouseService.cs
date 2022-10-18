using BusinessObject.Models;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IHouseService
    {
        Task<Response<House>> GetHouseById(string id);
        Task<Response<List<House>>> GetHouseListByCustomerId(string customerId, int page, int pageSize);
        Task<Response<House>> AddHouseForCustomer(House house);
        Task<Response<House>> UpdateHouseForCustomer(House house);
        Task<Response<House>> DeleteHouseForCustomer(House house);
        Task<Response<int>> CountHousesByCustomerId(int customerId);
    }
}
