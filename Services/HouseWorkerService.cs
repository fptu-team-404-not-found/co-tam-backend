﻿using BusinessObject.Models;
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
        private readonly IOrderRepository _orderRepository;

        public HouseWorkerService(IHouseWorkerRepository houseWorkerRepository, HouseWorkerValidation houseWorkerValidation, IOrderRepository orderRepository)
        {
            _houseWorkerRepository = houseWorkerRepository;
            _houseWorkerValidation = houseWorkerValidation;
            _orderRepository = orderRepository;
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

        public async Task<Response<int>> CountHouseworkerWhenSearch(string searchString)
        {
            try
            {
                var count = _houseWorkerRepository.CountHouseworkerWhenSearch(searchString);
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng houseworker không tồn tại",
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

        public async Task<Response<string>> CreateNewHouseWorker(HouseWorker houseWorker)
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
            _houseWorkerRepository.CreateNewHouseWorker(houseWorker);
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

        public async Task<Response<List<HouseWorker>>> GetListHouseworkerForManagerToAssign(int orderId)
        {
            try
            {
                if (orderId <= 0)
                {
                    return new Response<List<HouseWorker>>
                    {
                        Message = "Hãy nhập giá trị orderId lớn hơn 0",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkOrderExist = _orderRepository.GetOrderById(orderId);
                if (checkOrderExist == null)
                {
                    return new Response<List<HouseWorker>>
                    {
                        Message = "Không tìm thấy order",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _houseWorkerRepository.GetListHouseworkerForManagerToAssign(checkOrderExist.Package.Service.Name, checkOrderExist.House.Building.AreaId);
                if (lst.Count() == 0)
                {
                    return new Response<List<HouseWorker>>
                    {
                        Message = "Danh sách rỗng",
                        Success = false,
                        StatusCode = 200
                    };
                }
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

        public async Task<Response<List<HouseWorker>>> SearchAccountHouseworker(string searchString, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    return new Response<List<HouseWorker>>
                    {
                        Message = "Hãy nhập gì đó để tìm kiếm",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _houseWorkerRepository.SearchAccountHouseworker(searchString, page, pageSize);
                if (lst.Count() == 0)
                {
                    return new Response<List<HouseWorker>>
                    {
                        Message = "Không tìm thấy",
                        Success = false,
                        StatusCode = 200
                    };
                }
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

        public async Task<Response<string>> UpdateHouseWorker(HouseWorker houseWorker)
        {
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
            if (houseWorker.Name != null)
            {
                updateAdd.Name = houseWorker.Name;
            }
            if (houseWorker.Phone != null)
            {
                updateAdd.Phone = houseWorker.Phone;
            }
            if (houseWorker.DateOfBirth != null)
            {
                updateAdd.DateOfBirth = houseWorker.DateOfBirth;
            }
            if (houseWorker.Email != null)
            {
                updateAdd.Email = houseWorker.Email;
            }
            if (houseWorker.LinkFacebook != null)
            {
                updateAdd.LinkFacebook = houseWorker.LinkFacebook;
            }
            if (houseWorker.Avatar != null)
            {
                updateAdd.Avatar = houseWorker.Avatar;
            }
            if (houseWorker.AreaId != 0)
            {
                updateAdd.AreaId = houseWorker.AreaId;
            }
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
