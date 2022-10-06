using BusinessObject.Models;
using Repositories.IRepositories;
using Services.IServices;
using Services.ValidationHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly AreaValidation _areaValidation;

        public AreaService(IAreaRepository areaRepository, AreaValidation areaValidation)
        {
            _areaRepository = areaRepository;
            _areaValidation = areaValidation;
        }
        public Task<Response<Area>> GetReponseAreaById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Area>>> GetReponseAreas(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Area>> GetReponseChangeStatusArea(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Area>> GetReponseUpdateArea(string id, Area area)
        {
            throw new NotImplementedException();
        }

        public Task<Response<int>> GetResponseAreaNumber()
        {
            throw new NotImplementedException();
        }

        public Task<Response<Area>> GetResponseCreateAnArea(Area area)
        {
            throw new NotImplementedException();
        }
    }
}
