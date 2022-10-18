using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IServiceRepository
    {
        Service GetServiceById(int id);
        List<Service> GetAll(int page, int pageSize);
        void CreateAService(Service service);
        void UpdateAService(Service service);
        void DeleteAService(Service service);
    }
}
