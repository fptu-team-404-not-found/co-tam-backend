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
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly PackageValidation _packageValidation;

        public PackageService(IPackageRepository packageRepository, IServiceRepository serviceRepository, PackageValidation packageValidation)
        {
            _packageRepository = packageRepository;
            _serviceRepository = serviceRepository;
            _packageValidation = packageValidation;
        }
        public async Task<Response<int>> CountPackageByServiceId(int serviceId)
        {
            try
            {
                var checkServiceExist = _serviceRepository.GetServiceById(serviceId);
                if (checkServiceExist == null)
                {
                    return new Response<int>
                    {
                        Message = "Không tìm thấy service",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var count = _packageRepository.CountPackageByServiceId(serviceId);
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng package trong service không tồn tại",
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

        public async Task<Response<string>> CreateNewPackage(Package package)
        {
            try
            {
                var validate = _packageValidation.CheckCreateNewPackageValidation(package);
                if (validate != "ok")
                {
                    return new Response<string>
                    {
                        Message = validate,
                        Success = false,
                        StatusCode = 400
                    };
                }
                _packageRepository.CreateNewPackage(package);
                return new Response<string>
                {
                    Data = package.Id.ToString(),
                    Message = "Tạo mới thành công",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> DisableOrEnablePackage(int packageId)
        {
            try
            {
                var checkExist = _packageRepository.GetPackageById(packageId);
                if (checkExist == null)
                {
                    return new Response<string>
                    {
                        Message = "Không tìm thấy Package",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var result = _packageRepository.DisableOrEnablePackage(packageId);
                if (result)
                {
                    return new Response<string>
                    {
                        Message = "Đã Thực Hiện Thành Công Thao Tác Disable/Enable Package",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Message = "Disable/Enable Package Thất Bại",
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

        public async Task<Response<List<Package>>> GetAllPackageByEachServiceWithPagination(int serviceId, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var checkServiceExist = _serviceRepository.GetServiceById(serviceId);
                if (checkServiceExist == null)
                {
                    return new Response<List<Package>> { 
                        Message = "Không tìm thấy service",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _packageRepository.GetAllPackageByEachServiceWithPagination(serviceId, page, pageSize);
                return new Response<List<Package>>
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

        public async Task<Response<Package>> GetPackageById(int id)
        {
            try
            {
                var package = _packageRepository.GetPackageById(id);
                if (package != null)
                {
                    return new Response<Package>
                    {
                        Data = package,
                        Message = "Thành Công",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new Response<Package>
                    {
                        Message = "Không tìm thấy Package có id là " + id,
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

        public async Task<Response<Package>> UpdatePackage(Package package)
        {
            try
            {
                if (string.IsNullOrEmpty(package.Id.ToString()))
                {
                    return new Response<Package>
                    {
                        Message = "Hãy nhập Id",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var validate = _packageValidation.CheckCreateNewPackageValidation(package);
                if (validate != "ok")
                {
                    return new Response<Package> { 
                        Message = validate,
                        Success = false,
                        StatusCode = 400
                    };
                }
                var checkExist = _packageRepository.GetPackageById(package.Id);
                if (checkExist == null)
                {
                    return new Response<Package>
                    {
                        Message = "Không tìm thấy package",
                        Success = false,
                        StatusCode = 400
                    };
                }
                checkExist.NumberOfWorker = package.NumberOfWorker;
                checkExist.Duration = package.Duration;
                checkExist.ServiceId = package.ServiceId;
                _packageRepository.UpdatePackage(checkExist);
                return new Response<Package>
                {
                    Data = checkExist,
                    Message = "Update thành công",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
