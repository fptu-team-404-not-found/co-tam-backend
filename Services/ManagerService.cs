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

        public async Task<Response<string>> CountManager()
        {
            try
            {
                var count = _managerRepository.CountManager();
                if (count == 0)
                {
                    return new Response<string>
                    {
                        Message = "Số lượng manager không tồn tại",
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
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<int>> CountManagerWhenSearch(string searchString)
        {
            try
            {
                var count = _managerRepository.CountManagerWhenSearch(searchString);
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng manager không tồn tại",
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
            /*            AdminManager addManager = new AdminManager();
                        addManager.Name = manager.Name;
                        addManager.Phone = manager.Phone;
                        addManager.DateOfBirth = manager.DateOfBirth;
                        addManager.Email = manager.Email;
                        addManager.LinkFacebook = manager.LinkFacebook;
                        addManager.Avatar = manager.Avatar;
                        addManager.Active = manager.Active;*/
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
                var checkExist = _managerRepository.GetManager(managerId);
                if (checkExist == null)
                {
                    return new Response<string> { 
                        Message = "Không tìm thấy manager",
                        Success = false,
                        StatusCode = 400
                    };
                }
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

        public async Task<Response<List<AdminManager>>> GetAllManagerWithPagination(int pageIndex, int pageSize)
        {
            try
            {
                if (pageIndex <= 1)
                {
                    pageIndex = 1;
                }
                var lst = _managerRepository.GetAllManagerWithPagination(pageIndex, pageSize);
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

        public async Task<Response<List<AdminManager>>> SearchAccount(string searchString, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    return new Response<List<AdminManager>>
                    { 
                        Message = "Hãy nhập gì đó để tìm kiếm",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _managerRepository.SearchAccount(searchString, page, pageSize);
                if (lst.Count() == 0)
                {
                    return new Response<List<AdminManager>>
                    {
                        Message = "Không tìm thấy",
                        Success = false,
                        StatusCode = 200
                    };
                }
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

        public async Task<Response<string>> UpdateManager(AdminManager manager)
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
            var updateMan = _managerRepository.GetManager(manager.Id);
            if (updateMan == null)
            {
                return new Response<string>
                {
                    Message = "Tài khoản không tồn tại",
                    Success = false,
                    StatusCode = 400
                };
            }
            updateMan.Name = manager.Name;
            updateMan.Phone = manager.Phone;
            updateMan.DateOfBirth = manager.DateOfBirth;
            updateMan.Email = manager.Email;
            updateMan.LinkFacebook = manager.LinkFacebook;
            updateMan.Avatar = manager.Avatar;
            _managerRepository.UpdateManager(updateMan);
            return new Response<string>
            {
                Data = manager.Id.ToString(),
                Message = "Update Manager Thành Công",
                Success = true,
                StatusCode = 201
            };
        }
    }
}
