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

        public void DeleteHouse(int houseId)
        {
            try
            {
                var house = _cotamContext.Houses.FirstOrDefault(x => x.Id == houseId);
                if (house != null)
                {
                    if (house.Active == true)
                    {
                        house.Active = false;
                        _cotamContext.SaveChanges();
                    }
                    else if (house.Active == false)
                    {
                        house.Active = true;
                        _cotamContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public House GetHouseById(int id)
        {
            try
            {
                var house = _cotamContext.Houses
                    .Include(x => x.Building)
                    .Include(x => x.Customer)
                    .FirstOrDefault(house => house.Id == id);
                if (house != null)
                {
                    return house;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<House> GetListByCustomerId(int customerId, int page, int pageSize)
        {
            List<House> houses = new List<House> ();
            try
            {
                /*houses = _cotamContext.Houses.Where(h => h.CustomerId == customerId).ToList();*/
                var list = _cotamContext.Houses.Include(x => x.Building).Include(x => x.Customer).Where(h => h.CustomerId == customerId).Skip((page - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
                return list;
                /*return houses;*/
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
                _cotamContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<House> GetHouseByCusId(int cusId)
        {
            try
            {
                var house = _cotamContext.Houses
                    .Include(x => x.Customer)
                    .Include(x => x.Building)
                    .Where(x => x.CustomerId == cusId)
                    .ToList();
                if (house != null)
                {
                    return house;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
