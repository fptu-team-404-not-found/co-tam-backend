using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IAreaRepository
    {
        Area GetAreaById(int id);

        List<Area> GetAreaList(int pageIndex, int pageSize);
        void CreateArea(Area area);
        void UpdateArea(Area area);
        void ChangeAreaStatus(Area area);
        int CountAreas();
    }
}
