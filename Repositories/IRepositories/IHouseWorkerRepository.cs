using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IHouseWorkerRepository
    {
        List<HouseWorker> GetAllHouseWorkerWithPagination(int page, int pageSize);
        int CountHouseWorker();
        HouseWorker GetHouseWorkerById(int id);
        bool DisableOrEnableHouseWorkerAccount(int id);
        void CreateNewHouseWorker(HouseWorker houseWorker, int managerId);
        void UpdateHouseWorker(HouseWorker houseWorker);
    }
}
