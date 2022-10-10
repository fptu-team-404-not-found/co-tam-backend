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
    public class HouseWorkerService : IHouseWorkerService
    {
        private readonly IHouseWorkerRepository _houseWorkerRepository;
        private readonly HouseWorkerValidation _houseWorkerValidation;

        public HouseWorkerService(IHouseWorkerRepository houseWorkerRepository, HouseWorkerValidation houseWorkerValidation)
        {
            _houseWorkerRepository = houseWorkerRepository;
            _houseWorkerValidation = houseWorkerValidation;
        }
        public async Task<Response<string>> CountHouseWorker()
        {
            try
            {
                var count = _houseWorkerRepository.CountHouseWorker();
                if (count == 0)
                {
                    return new Response<string>
                    {
                        Message = "Số lượng houseworker không tồn tại",
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

        public async Task<Response<string>> CreateNewHouseWorker(HouseWorker houseWorker, int managerId)
        {
            var validate = _houseWorkerValidation.CheckCreateNewHouseWorker(houseWorker);
            if (validate != "ok")
            {
                return new Response<string>
                {
                    Message = validate,
                    Success = false,
                    StatusCode = 400
                };
            }
            _houseWorkerRepository.CreateNewHouseWorker(houseWorker, managerId);
            return new Response<string>
            {
                Data = houseWorker.Id.ToString(),
                Message = "Tạo mới houseworker thành công",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<Response<string>> DisableOrEnableHouseWorkerAccount(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = "Id không tồn tại",
                        StatusCode = 400
                    };
                }
                var checkAccount = _houseWorkerRepository.GetHouseWorkerById(id);
                if (checkAccount == null)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = "Không tìm thấy houseworker",
                        StatusCode = 400
                    };
                }
                var result = _houseWorkerRepository.DisableOrEnableHouseWorkerAccount(id);
                if (result == false)
                {
                    return new Response<string>
                    {
                        Success = false,
                        Message = "Không thể disable hay enable account houseworker",
                        StatusCode = 400
                    };
                }
                return new Response<string>
                {
                    Success = true,
                    Message = "Thành Công",
                    StatusCode = 200
                };
            }
            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<HouseWorker>>> GetAllHouseWorkerWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = _houseWorkerRepository.GetAllHouseWorkerWithPagination(page, pageSize);
                return new Response<List<HouseWorker>>
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

        public async Task<Response<HouseWorker>> GetHouseWorkerById(int id)
        {
            if (id <= 0)
            {
                return new Response<HouseWorker>
                {
                    Success = false,
                    Message = "Id không tồn tại",
                    StatusCode = 400
                };
            }
            var houseworker = _houseWorkerRepository.GetHouseWorkerById(id);
            if (houseworker == null)
            {
                return new Response<HouseWorker>
                {
                    Success = false,
                    Message = "Không tìm thấy houseworker",
                    StatusCode = 400
                };
            }
            return new Response<HouseWorker>
            {
                Data = houseworker,
                Success = true,
                Message = "Thành Công",
                StatusCode = 200
            };
        }

        public async Task<Response<string>> UpdateHouseWorker(HouseWorker houseWorker)
        {
            var validate = _houseWorkerValidation.CheckCreateNewHouseWorker(houseWorker);
            if (validate != "ok")
            {
                return new Response<string>
                {
                    Message = validate,
                    Success = false,
                    StatusCode = 400
                };
            }
            var updateAdd = _houseWorkerRepository.GetHouseWorkerById(houseWorker.Id);
            if (updateAdd == null)
            {
                return new Response<string>
                {
                    Message = "Tài khoản không tồn tại",
                    Success = false,
                    StatusCode = 400
                };
            }
            updateAdd.Name = houseWorker.Name;
            updateAdd.Phone = houseWorker.Phone;
            updateAdd.DateOfBirth = houseWorker.DateOfBirth;
            updateAdd.Email = houseWorker.Email;
            updateAdd.LinkFacebook = houseWorker.LinkFacebook;
            updateAdd.Avatar = houseWorker.Avatar;
            _houseWorkerRepository.UpdateHouseWorker(updateAdd);
            return new Response<string>
            {
                Data = houseWorker.Id.ToString(),
                Message = "Update Housworker Thành Công",
                Success = true,
                StatusCode = 201
            };
        }
    }
}
