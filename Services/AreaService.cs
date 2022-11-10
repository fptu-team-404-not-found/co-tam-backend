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

        public async Task<Response<int>> CountAreasWhenSearch(string searchString)
        {
            try
            {
                var count = _areaRepository.CountAreasWhenSearch(searchString);
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng area không tồn tại",
                        Success = false
                    };
                }
                return new Response<int>
                {
                    Data = count,
                    Message = "Thành Công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Area>> GetReponseAreaById(string id)
        {
            try
            {
                int _id = _areaValidation.ValidationId(id);
                if (_id < 0)
                {
                    return new Response<Area>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Area area = _areaRepository.GetAreaById(_id);
                if (area == null)
                {
                    return new Response<Area>
                    {
                        Message = "Khu vực không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<Area>
                {
                    Data = area,
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<Area>>> GetReponseAreas(int pageIndex, int pageSize)
        {
            try
            {
                List<Area> areas = _areaRepository.GetAreaList(pageIndex, pageSize);

                if (areas == null)
                {
                    return new Response<List<Area>>
                    {
                        Message = "Danh sách khu vực không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<List<Area>>
                {
                    Data = areas,
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Area>> GetReponseChangeStatusArea(string id)
        {
            try
            {
                int _id = _areaValidation.ValidationId(id);
                if (_id < 0)
                {
                    return new Response<Area>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Area area = _areaRepository.GetAreaById(_id);
                if (area == null)
                {
                    return new Response<Area>
                    {
                        Message = "Khu vực không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                _areaRepository.ChangeAreaStatus(area);

                return new Response<Area>
                {
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Area>> GetReponseUpdateArea(string id, Area area)
        {
            try
            {
                int _id = _areaValidation.ValidationId(id);
                if (_id < 0)
                {
                    return new Response<Area>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Area _area = _areaRepository.GetAreaById(int.Parse(id));
                if (_area == null)
                {
                    return new Response<Area>
                    {
                        Message = "Khu vực không tồn tại!",
                        Success = false,
                        StatusCode = 404,
                    };
                }

                area.Id = int.Parse(id);
                _areaRepository.UpdateArea(area);

                return new Response<Area>
                {
                    Data = area,
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<int>> GetResponseAreaNumber()
        {
            try
            {
                int areaNumber = _areaRepository.CountAreas();
                return new Response<int>
                {
                    Data = areaNumber,
                    Message = "Thành công",
                    StatusCode = 200,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Area>> GetResponseCreateAnArea(Area area)
        {
            try
            {
                if (area.Id != 0)
                {
                    return new Response<Area>
                    {
                        Message = "Không cần thêm Id khi tạo 1 khu vực mới",
                        Success = false,
                        StatusCode = 400
                    };
                }

                _areaRepository.CreateArea(area);
                return new Response<Area>
                {
                    Data = area,
                    Message = "Thành công",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<Area>>> SearchArea(string searchString, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    return new Response<List<Area>>
                    {
                        Message = "Hãy nhập gì đó để tìm kiếm",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _areaRepository.SearchArea(searchString, page, pageSize);
                if (lst == null)
                {
                    return new Response<List<Area>>
                    {
                        Message = "Không tìm thấy",
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<List<Area>>
                {
                    Data = lst,
                    Message = "Thành Công",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
