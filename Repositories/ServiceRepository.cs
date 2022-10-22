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
    public class ServiceRepository : IServiceRepository
    {
        private readonly cotamContext _cotamContext;

        public ServiceRepository(cotamContext cotamContext)
        {
            _cotamContext = cotamContext;
        }

        public void CreateAService(Service service)
        {
            try
            {
                _cotamContext.Services.Add(service);
                _cotamContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteAService(Service service)
        {
            try
            {
                if (service.Active == 1)
                {
                    service.Active = 0;
                    _cotamContext.SaveChanges();
                }
                else if (service.Active == 0)
                {
                    service.Active = 1;
                    _cotamContext.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Service> GetAll(int page, int pageSize)
        {
            List<Service> services = new List<Service>();
            try
            {
                var list = _cotamContext.Services
                        .Skip((page - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public void UpdateAService(Service service)
        {
            try
            {
                _cotamContext.ChangeTracker.Clear();
                _cotamContext.Entry(service).State = EntityState.Modified;
                _cotamContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
