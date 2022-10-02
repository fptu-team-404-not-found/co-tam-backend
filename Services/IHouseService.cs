using BusinessObject.Models;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IHouseService
    {
        Task<Response<House>> GetHouseById(int id);
    }
}
