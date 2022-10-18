using BusinessObject.Models;
using Repositories;
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
    public class ExtraServiceService : IExtraServiceService
    {
        private readonly IExtraServiceRepository _extraServiceRepository;
        private readonly ExtraServiceValidation _extraServiceValidation;
        private readonly IServiceRepository _serviceRepository;

        public ExtraServiceService(IExtraServiceRepository extraServiceRepository, ExtraServiceValidation extraServiceValidation, IServiceRepository serviceRepository)
        {
            _extraServiceRepository = extraServiceRepository;
            _extraServiceValidation = extraServiceValidation;
            _serviceRepository = serviceRepository;
        }

        public async Task<Response<List<ExtraService>>> GetReponseExtraServices(int serId, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var checkExist = _serviceRepository.GetServiceById(serId);
                if (checkExist == null)
                {
                    return new Response<List<ExtraService>>
                    {
                        Message = "Không tìm thấy service",
                        StatusCode = 400,
                        Success = false
                    };
                }
                List<ExtraService> extraServices = _extraServiceRepository.GetAll(serId, page, pageSize);

                if (extraServices == null)
                {
                    return new Response<List<ExtraService>>
                    {
                        Message = "Danh sách dịch vụ đi kèm không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<List<ExtraService>>
                {
                    Data = extraServices,
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

        public async Task<Response<ExtraService>> GetReponseExtraServiceById(string id)
        {
            try
            {
                int _id = _extraServiceValidation.ValidateId(id);
                if (_id < 0)
                {
                    return new Response<ExtraService>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                ExtraService extraService = _extraServiceRepository.GetExtraServiceById(_id);

                if (extraService == null)
                {
                    return new Response<ExtraService>
                    {
                        Message = "Dịch vụ đi kèm không tồn tại!",
                        Success = false,
                        StatusCode = 422
                    };
                }

                return new Response<ExtraService>
                {
                    Data = extraService,
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

        public async Task<Response<ExtraService>> GetResponseCreateAExtraService(ExtraService extraService)
        {
            try
            {
                if (extraService.Id != 0)
                {
                    return new Response<ExtraService>
                    {
                        Message = "Không cần thêm Id khi tạo 1 dịch vụ đi kèm mới",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Service _service = _serviceRepository.GetServiceById(extraService.ServiceId);
                if (_service == null)
                {
                    return new Response<ExtraService>
                    {
                        Message = "Dịch vụ không tồn tại!",
                        Success = false,
                        StatusCode = 404,
                    };
                }

                _extraServiceRepository.CreateAExtraService(extraService);
                return new Response<ExtraService>
                {
                    Data = extraService,
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

        public async Task<Response<ExtraService>> GetReponseDeleteExtraService(string id)
        {
            try
            {
                int _id = _extraServiceValidation.ValidateId(id);
                if (_id < 0)
                {
                    return new Response<ExtraService>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                ExtraService extraService = _extraServiceRepository.GetExtraServiceById(_id);
                if (extraService == null)
                {
                    return new Response<ExtraService>
                    {
                        Message = "Dịch vụ đi kèm không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                extraService.Active = 0;
                _extraServiceRepository.DeleteAExtraService(extraService);

                return new Response<ExtraService>
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
