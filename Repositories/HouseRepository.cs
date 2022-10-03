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

        public int CountHouse(int customerId)
        {
            int count = 0;
            try
            {
                count = _cotamContext.Houses.Count(h => h.CustomerId == customerId);
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreateHouse(House house)
        {
            try
            {
                if (_cotamContext.Customers.FirstOrDefault(h => h.Id == house.CustomerId) == null)
                {
                    return false;
                }
                if (_cotamContext.Buildings.FirstOrDefault(h => h.Id != house.Id) == null)
                {
                    return false;
                }
                _cotamContext.Houses.Add(house);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteHouse(House house)
        {
            try
            {
                _cotamContext.Houses.Update(house);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public House GetHouseById(int id)
        {
            House house = null;
            try
            {
                house = _cotamContext.Houses.FirstOrDefault(house => house.Id == id);
                return house;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<House> GetListByCustomerId(int customerId)
        {
            List<House> houses = new List<House> ();
            try
            {
                houses = _cotamContext.Houses.Where(h => h.CustomerId == customerId).ToList();
                return houses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateHouse(House house)
        {
            try
            {
                _cotamContext.Houses.Update(house);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
