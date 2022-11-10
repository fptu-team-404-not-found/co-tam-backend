using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IBuildingRepository
    {
        List<Building> GetAllBuildingWithPagination(int page, int pageSize);
        int CountBuilding();
        Building GetBuildingById(int id);
        bool DisableOrEnableBuilding(int id);
        void CreateNewBuilding(Building building);
        void UpdateBuilding(Building building);
        List<Building> SearchBuilding(string searchString, int page, int pageSize);
        int CountBuildingsWhenSearch(string searchString);
    }
}
