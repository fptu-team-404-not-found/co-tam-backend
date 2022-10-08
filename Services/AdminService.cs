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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly AdminValidation _adminValidation;

        public AdminService(IAdminRepository adminRepository, AdminValidation adminValidation)
        {
            _adminRepository = adminRepository;
            _adminValidation = adminValidation;
        }

        public async Task<Response<string>> CountAdmin()
        {
            try
            {
                var count = _adminRepository.CountAdmin();
                if (count == 0)
                {
                    return new Response<string>
                    {
                        Message = "Số lượng admin không tồn tại",
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

        public async Task<Response<string>> CreateNewAdmin(AdminManager admin)
        {
            var validate = _adminValidation.CheckCreateNewAdmin(admin);
            if (validate != "ok")
            {
                return new Response<string>
                {
                    Message = validate,
                    Success = false,
                    StatusCode = 400
                };
            }
            /* AdminManager addAdmin = new AdminManager();
             addAdmin.Name = admin.Name;
             addAdmin.Phone = admin.Phone;
             addAdmin.DateOfBirth = admin.DateOfBirth;
             addAdmin.Email = admin.Email;
             addAdmin.LinkFacebook = admin.LinkFacebook;
             addAdmin.Avatar = admin.Avatar;
             addAdmin.Active = admin.Active;
             addAdmin.RoleId = 1;*/
            admin.RoleId = 1;


            _adminRepository.CreateNewAdmin(admin);
            return new Response<string>
            {
                Data = admin.Id.ToString(),
                Message = "Tạo Mới Admin Thành Công",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<Response<string>> DisableOrEnableAdmin(int adminId)
        {
            try
            {
                var result = _adminRepository.DisableOrEnableAdmin(adminId);
                if (result)
                {
                    return new Response<string>
                    {
                        Message = "Đã Thực Hiện Thành Công Thao Tác Disable/Enable AdminAccount",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else {
                    return new Response<string>
                    {
                        Message = "Disable/Enable AdminAccount Thất Bại",
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

        public async Task<Response<AdminManager>> GetAdmin_ManagerById(int id)
        {
            try
            {
                var ad = _adminRepository.GetAdmin_ManagerById(id);
                if (ad != null)
                {
                    return new Response<AdminManager>
                    {
                        Data = ad,
                        Message = "Thành Công",
                        Success = true
                    };
                }
                else
                {
                    return new Response<AdminManager>
                    {
                        Message = "Không tìm thấy Admin có id là " + id,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<AdminManager>>> GetAllAdminWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = _adminRepository.GetAllAdminWithPagination(page, pageSize);
                return new Response<List<AdminManager>>
                {
                    Data = lst,
                    Message = "Thành Công",
                    Success = true,
                    StatusCode = 200
                };
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> UpdateAdmin(AdminManager admin)
        {
            var validate = _adminValidation.CheckCreateNewAdmin(admin);
            if (validate != "ok")
            {
                return new Response<string>
                {
                    Message = validate,
                    Success = false,
                    StatusCode = 400
                };
            }
            var updateAdd = _adminRepository.GetAdmin_ManagerById(admin.Id);
            if (updateAdd == null)
            {
                return new Response<string>
                {
                    Message = "Tài khoản không tồn tại",
                    Success = false,
                    StatusCode = 400
                };
            }
            updateAdd.Name = admin.Name;
            updateAdd.Phone = admin.Phone;
            updateAdd.DateOfBirth = admin.DateOfBirth;
            updateAdd.Email = admin.Email;
            updateAdd.LinkFacebook = admin.LinkFacebook;
            updateAdd.Avatar = admin.Avatar;
            _adminRepository.UpdateAdmin(updateAdd);
            return new Response<string>
            {
                Data = admin.Id.ToString(),
                Message = "Update Admin Thành Công",
                Success = true,
                StatusCode = 201
            };
        }
    }
}
