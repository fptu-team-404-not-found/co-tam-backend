using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly cotamContext _dbContext;

        public BuildingRepository(cotamContext context)
        {
            _dbContext = context;
        }
        public int CountBuilding()
        {
            try
            {
                var count = _dbContext.Buildings.Count();
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateNewBuilding(Building building)
        {
            try
            {
                _dbContext.Buildings.Add(building);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DisableOrEnableBuilding(int id)
        {
            try
            {
                var building = _dbContext.Buildings.FirstOrDefault(x => x.Id == id);
                if (building != null)
                {
                    if (building.Active == true)
                    {
                        building.Active = false;
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else if (building.Active == false)
                    {
                        building.Active = true;
                        _dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Building> GetAllBuildingWithPagination(int page, int pageSize)
        {
            try
            {
                var list = _dbContext.Buildings
                        .Include( x=> x.Area).Skip((page - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public Building GetBuildingById(int id)
        {
            try
            {
                var building = _dbContext.Buildings.Include(x => x.Area).FirstOrDefault( x => x.Id == id);
                if (building != null)
                {
                    return building;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateBuilding(Building building)
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
