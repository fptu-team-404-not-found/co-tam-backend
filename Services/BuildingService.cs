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
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly BuildingValidation _buildingValidation;

        public BuildingService(IBuildingRepository buildingRepository, BuildingValidation buildingValidation)
        {
            _buildingRepository = buildingRepository;
            _buildingValidation = buildingValidation;
        }
        public async Task<Response<string>> CountBuilding()
        {
            try
            {
                var count = _buildingRepository.CountBuilding();
                if (count == 0)
                {
                    return new Response<string>
                    {
                        Message = "Số lượng building không tồn tại",
                        Success = false
                    };
                }
                return new Response<string>
                {
                    Data = count.ToString(),
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

        public async Task<Response<int>> CountBuildingsWhenSearch(string searchString)
        {
            try
            {
                var count = _buildingRepository.CountBuildingsWhenSearch(searchString);
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng promotion không tồn tại",
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

        public async Task<Response<string>> CreateNewBuilding(Building building)
        {
            try
            {
                var validate = _buildingValidation.CheckCreateNewBuildingWithValidation(building);
                if (validate != "ok")
                {
                    return new Response<string>
                    {
                        Message = validate,
                        Success = false,
                        StatusCode = 400
                    };
                }
                building.Active = true;
                _buildingRepository.CreateNewBuilding(building);
                return new Response<string>
                {
                    Data = building.Id.ToString(),
                    Message = "Thành Công",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> DisableOrEnableBuilding(int id)
        {
            try
            {
                var checkExist = _buildingRepository.GetBuildingById(id);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy Building có id là " + id,
                        Success = false,
                        StatusCode = 400
                    };
                }
                var result = _buildingRepository.DisableOrEnableBuilding(id);
                if (result)
                {
                    return new Response<string>
                    {
                        Message = "Đã Thực Hiện Thành Công Thao Tác Disable/Enable Building",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Message = "Disable/Enable Building Thất Bại",
                        Success = false,
                        StatusCode = 405
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<Building>>> GetAllBuildingWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = _buildingRepository.GetAllBuildingWithPagination(page, pageSize);
                return new Response<List<Building>>
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

        public async Task<Response<Building>> GetBuildingById(int id)
        {
            try
            {
                var building = _buildingRepository.GetBuildingById(id);
                if (building != null)
                {
                    return new Response<Building>
                    {
                        Data = building,
                        Message = "Thành Công",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new Response<Building>
                    {
                        Message = "Không tìm thấy Building có id là " + id,
                        Success = false, 
                        StatusCode = 400
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<Building>>> SearchBuilding(string searchString, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    return new Response<List<Building>>
                    {
                        Message = "Hãy nhập gì đó để tìm kiếm",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _buildingRepository.SearchBuilding(searchString, page, pageSize);
                if (lst == null)
                {
                    return new Response<List<Building>>
                    {
                        Message = "Không tìm thấy",
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<List<Building>>
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

        public async Task<Response<string>> UpdateBuilding(Building building)
        {
            try
            {
                var validate = _buildingValidation.CheckCreateNewBuildingWithValidation(building);
                if (validate != "ok")
                {
                    return new Response<string>
                    {
                        Message = validate,
                        Success = false,
                        StatusCode = 400
                    };
                }

                var buildingExist = _buildingRepository.GetBuildingById(building.Id);
                if (buildingExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Building không tồn tại!",
                        Success = false,
                        StatusCode = 404,
                    };
                }

                buildingExist.Name = building.Name;
                buildingExist.AreaId = building.AreaId;
                _buildingRepository.UpdateBuilding(buildingExist);

                return new Response<string>
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
    }
}
