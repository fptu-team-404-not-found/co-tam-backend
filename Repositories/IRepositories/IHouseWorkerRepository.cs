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
        void CreateNewHouseWorker(HouseWorker houseWorker);
        void UpdateHouseWorker(HouseWorker houseWorker);
        List<HouseWorker> SearchAccountHouseworker(string searchString, int page, int pageSize);
        int CountHouseworkerWhenSearch(string searchString);
        List<HouseWorker> GetListHouseworkerForManagerToAssign(string workerTagName, int areaId);
    }
}
