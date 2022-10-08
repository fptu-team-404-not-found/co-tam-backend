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
    public class AreaRepository : IAreaRepository
    {
        private readonly cotamContext _cotamContext;

        public AreaRepository(cotamContext cotamContext)
        {
            _cotamContext = cotamContext;
        }
        public void ChangeAreaStatus(Area area)
        {
            try
            {
                if (area.Active == false)
                {
                    area.Active = true;
                }
                else
                {
                    area.Active = false;
                }
                _cotamContext.Areas.Update(area);
                _cotamContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountAreas()
        {
            try
            {
                return _cotamContext.Areas.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateArea(Area area)
        {
            try
            {
                _cotamContext.Areas.Add(area);
                _cotamContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Area GetAreaById(int id)
        {
            Area area = null;
            try
            {
                area = _cotamContext.Areas.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return area;
        }

        public List<Area> GetAreaList(int pageIndex, int pageSize)
        {
            var list = _cotamContext.Areas
                        .Skip((pageIndex - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
            return list;
        }

        public void UpdateArea(Area area)
        {
            try
            {
                _cotamContext.ChangeTracker.Clear();
                _cotamContext.Entry(area).State = EntityState.Modified;
                _cotamContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
