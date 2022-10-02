using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepositories;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly cotamContext _cotamContext;

        public HouseRepository(cotamContext cotamContext)
        {
            _cotamContext = cotamContext;
        }
        public House GetHouseById(int id)
        {
            var house = _cotamContext.Houses.FirstOrDefault(house => house.Id == id);
            return house;
        }
    }
}
