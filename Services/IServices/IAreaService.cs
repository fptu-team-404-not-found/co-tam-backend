using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAreaService
    {
        Task<Response<Area>> GetReponseAreaById(string id);
        Task<Response<List<Area>>> GetReponseAreas(int pageIndex, int pageSize);
        Task<Response<Area>> GetResponseCreateAnArea(Area area);
        Task<Response<Area>> GetReponseUpdateArea(string id, Area area);
        Task<Response<Area>> GetReponseChangeStatusArea(string id);
        Task<Response<int>> GetResponseAreaNumber();
        Task<Response<List<Area>>> SearchArea(string searchString, int page, int pageSize);
        Task<Response<int>> CountAreasWhenSearch(string searchString);
    }
}
