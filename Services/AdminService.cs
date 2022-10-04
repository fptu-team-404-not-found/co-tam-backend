﻿using BusinessObject.Models;
using Repositories.IRepositories;
using Services.IServices;
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

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
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

        public async Task<Response<List<AdminManager>>> GetAllAdminWithPagination(int page)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = _adminRepository.GetAllAdminWithPagination(page);
                return new Response<List<AdminManager>>
                {
                    Data = lst,
                    Message = "Thành Công",
                    Success = true
                };
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
