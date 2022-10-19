using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IHouseWorkerService
    {
        Task<Response<List<HouseWorker>>> GetAllHouseWorkerWithPagination(int page, int pageSize);
        Task<Response<string>> CountHouseWorker();
        Task<Response<HouseWorker>> GetHouseWorkerById(int id);
        Task<Response<string>> DisableOrEnableHouseWorkerAccount(int id);
        Task<Response<string>> CreateNewHouseWorker(HouseWorker houseWorker);
        Task<Response<string>> UpdateHouseWorker(HouseWorker houseWorker);
    }
}
