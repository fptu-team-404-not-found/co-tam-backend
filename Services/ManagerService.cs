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
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly ManagerValidation _managerValidation;

        public ManagerService(IManagerRepository managerRepository, ManagerValidation managerValidation)
        {
            _managerRepository = managerRepository;
            _managerValidation = managerValidation;
        }

        public async Task<Response<string>> CreateNewManager(AdminManager manager)
        {
            var validate = _managerValidation.CheckCreateNewManager(manager);
            if (validate != "ok")
            {
                return new Response<string>
                {
                    Message = validate,
                    Success = false,
                    StatusCode = 400
                };
            }
            manager.RoleId = 2;
            _managerRepository.CreateNewManager(manager);
            return new Response<string>
            {
                Data = manager.Id.ToString(),
                Message = "Tạo Mới Manager Thành Công",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<Response<string>> DisableOrEnableManager(int managerId)
        {
            try
            {
                var result = _managerRepository.DisableOrEnableManager(managerId);
                if (result)
                {
                    return new Response<string>
                    {
                        Message = "Đã Thực Hiện Thành Công Thao Tác Disable/Enable ManagerAccount",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Message = "Disable/Enable ManagerAccount Thất Bại",
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

        public async Task<Response<List<AdminManager>>> GetAllManagerWithPagination(int page)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = _managerRepository.GetAllManagerWithPagination(page);
                return new Response<List<AdminManager>>
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

        public async Task<Response<AdminManager>> GetManager(int managerId)
        {
            try
            {
                var manager = _managerRepository.GetManager(managerId);
                if (manager != null)
                {
                    return new Response<AdminManager>
                    {
                        Data = manager,
                        Message = "Thành Công",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else 
                {
                    return new Response<AdminManager>
                    {
                        Message = "Không tìm thấy Manager",
                        Success = false,
                        StatusCode = 404
                    };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
