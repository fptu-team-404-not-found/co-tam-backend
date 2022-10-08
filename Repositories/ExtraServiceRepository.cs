using BusinessObject.Models;
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

        public List<ExtraService> GetAll()
        {
            List<ExtraService> extraServices = new List<ExtraService>();
            try
            {
                extraServices = _cotamContext.ExtraServices.Where(x => x.Active == 1).ToList();
                return extraServices;
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
    }
}
