using BusinessObject.Models;
using Repositories.IRepositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WorkerTagService : IWorkerTagService
    {
        private readonly IWorkerTagRepository _workerTagRepository;
        private readonly IHouseWorkerRepository _houseWorkerRepository;

        public WorkerTagService(IWorkerTagRepository workerTagRepository, IHouseWorkerRepository houseWorkerRepository)
        {
            _workerTagRepository = workerTagRepository;
            _houseWorkerRepository = houseWorkerRepository;
        }
        public async Task<Response<int>> CountWorkerTag()
        {
            try
            {
                var count = _workerTagRepository.CountWorkerTag();
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng worker tag không tồn tại",
                        Success = false,
                        StatusCode = 400
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

        public async Task<Response<WorkerTag>> CreateNewWorkerTag(WorkerTag workerTag)
        {
            try
            {
                if (string.IsNullOrEmpty(workerTag.Name))
                { 
                    return new Response<WorkerTag>{
                        Message = "Xin Hãy Nhập Name",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExist = _workerTagRepository.CheckWorkerTagHasExist(workerTag.HouseWorkerId, workerTag.Name);
                if (checkExist != null)
                {
                    return new Response<WorkerTag>
                    {
                        Message = "TagName này houseworker đã có",
                        Success = false,
                        StatusCode = 400
                    };
                }
                _workerTagRepository.CreateNewWorkerTag(workerTag);
                return new Response<WorkerTag>
                {
                    Data = workerTag,
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

        public async Task<Response<List<WorkerTag>>> GetAllWorkerTagWithPagination(int pageIndex, int pageSize)
        {
            try
            {
                if (pageIndex <= 1)
                {
                    pageIndex = 1;
                }
                var lst = _workerTagRepository.GetAllWorkerTagWithPagination(pageIndex, pageSize);
                return new Response<List<WorkerTag>>
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

        public async Task<Response<List<WorkerTag>>> GetWorkerTag()
        {
            try
            {
                var lst = _workerTagRepository.GetWorkerTag();
                if (lst.Count() == 0)
                {
                    return new Response<List<WorkerTag>>
                    {
                        Message = "Danh sách rỗng",
                        Success = false,
                        StatusCode = 200
                    };
                }
                return new Response<List<WorkerTag>>
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

        public async Task<Response<WorkerTag>> GetWorkerTagById(int id)
        {
            try
            {
                var workerTag = _workerTagRepository.GetWorkerTagById(id);
                if (workerTag == null)
                {
                    return new Response<WorkerTag>
                    {
                        Message = "Không tìm thấy workerTag",
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<WorkerTag>
                {
                    Data = workerTag,
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

        public async Task<Response<List<WorkerTag>>> GetWorkerTagsByHouseworkerId(int houseworkerId)
        {
            try
            {
                if (string.IsNullOrEmpty(houseworkerId.ToString()))
                {
                    return new Response<List<WorkerTag>>
                    {
                        Message = "Hãy Nhập HouseworkerID",
                        StatusCode = 400,
                        Success = false
                    };
                }
                if (houseworkerId <= 0)
                {
                    return new Response<List<WorkerTag>>
                    {
                        Message = "Hãy Nhập HouseworkerID có giá trị lớn hơn 0",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var checkHouseworker = _houseWorkerRepository.GetHouseWorkerById(houseworkerId);
                if (checkHouseworker == null)
                {
                    return new Response<List<WorkerTag>>
                    {
                        Message = "Không tìm thấy houseworker",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var lst = _workerTagRepository.GetWorkerTagsByHouseworkerId(houseworkerId);
                if (lst.Count() == 0)
                {
                    return new Response<List<WorkerTag>>
                    {
                        Message = "Không tìm thấy danh sách workerTag của id này",
                        StatusCode = 200,
                        Success = false
                    };
                }
                return new Response<List<WorkerTag>>
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

        public async Task<Response<string>> RemoveWorkerTag(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return new Response<string>
                    {
                        Message = "Hãy Nhập id",
                        StatusCode = 400,
                        Success = false
                    };
                }
                if (id <= 0)
                {
                    return new Response<string>
                    {
                        Message = "Hãy Nhập id có giá trị lớn hơn 0",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var checkExist = _workerTagRepository.GetWorkerTagById(id);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy WorkerTag",
                        StatusCode = 400,
                        Success = false
                    };
                }
                _workerTagRepository.RemoveWorkerTag(id);
                return new Response<string>
                {
                    Message = "Thành Công",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<WorkerTag>> UpdateWorkerTag(WorkerTag workerTag)
        {
            try
            {
                if (workerTag.Id <= 0)
                {
                    return new Response<WorkerTag>
                    {
                        Message = "Hãy Nhập id có giá trị lớn hơn 0",
                        StatusCode = 400,
                        Success = false
                    };
                }
                var checkExist = _workerTagRepository.GetWorkerTagById(workerTag.Id);
                if (checkExist == null)
                {
                    return new Response<WorkerTag>
                    {
                        Message = "Không tìm thấy WorkerTag",
                        StatusCode = 400,
                        Success = false
                    };
                }
                checkExist.Name = workerTag.Name;
                checkExist.HouseWorkerId = workerTag.HouseWorkerId;
                _workerTagRepository.UpdateWorkerTag(checkExist);
                return new Response<WorkerTag>
                {
                    Message = "Thành Công",
                    StatusCode = 201,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
