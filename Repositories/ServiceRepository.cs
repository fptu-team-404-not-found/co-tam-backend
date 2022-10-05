using BusinessObject.Models;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly cotamContext _cotamContext;

        public ServiceRepository(cotamContext cotamContext)
        {
            _cotamContext = cotamContext;
        }

        public Service GetServiceById(int id)
        {
            Service service = null;
            try
            {
                service = _cotamContext.Services.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return service;
        }
    }
}
