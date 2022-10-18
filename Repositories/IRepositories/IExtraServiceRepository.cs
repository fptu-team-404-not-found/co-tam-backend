using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IExtraServiceRepository
    {
        List<ExtraService> GetAll(int serId, int page, int pageSize);
        ExtraService GetExtraServiceById(int id);
        void CreateAExtraService(ExtraService extraService);
        void DeleteAExtraService(ExtraService extraService);
    }
}
