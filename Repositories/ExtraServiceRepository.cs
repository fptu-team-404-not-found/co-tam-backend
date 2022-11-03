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
    public class ExtraServiceRepository : IExtraServiceRepository
    {
        private readonly cotamContext _cotamContext;

        public ExtraServiceRepository(cotamContext cotamContext)
        {
            _cotamContext = cotamContext;
        }

        public void CreateAExtraService(ExtraService extraService)
        {
            try
            {
                _cotamContext.ExtraServices.Add(extraService);
                _cotamContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAExtraService(ExtraService extraService)
        {
            try
            {
                _cotamContext.Update(extraService);
                _cotamContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ExtraService> GetAll(int serId, int page, int pageSize)
        {
            try
            {
                /*extraServices = _cotamContext.ExtraServices.Where(x => x.Active == 1).ToList();
                return extraServices;*/
                var list = _cotamContext.ExtraServices.Include(x => x.Service).Where(x => x.ServiceId == serId && x.Active == 1)
                       .Skip((page - 1) * (int)pageSize)
                       .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ExtraService GetExtraServiceById(int id)
        {
            ExtraService extraService = null;
            try
            {
                extraService = _cotamContext.ExtraServices.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return extraService;
        }
        public int CountExtraServiceByServiceId(int serviceId)
        {
            try
            {
                var count = _cotamContext.ExtraServices.Where(x => x.ServiceId == serviceId).Count();

                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
