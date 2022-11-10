using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IBuildingService
    {
        Task<Response<List<Building>>> GetAllBuildingWithPagination(int page, int pageSize);
        Task<Response<string>> CountBuilding();
        Task<Response<Building>> GetBuildingById(int id);
        Task<Response<string>> DisableOrEnableBuilding(int id);
        Task<Response<string>> CreateNewBuilding(Building building);
        Task<Response<string>> UpdateBuilding(Building building);
        Task<Response<List<Building>>> SearchBuilding(string searchString, int page, int pageSize);
        Task<Response<int>> CountBuildingsWhenSearch(string searchString);
    }
}
