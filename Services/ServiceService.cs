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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ServiceValidation _serviceValidation;

        public ServiceService(IServiceRepository serviceRepository, ServiceValidation serviceValidation)
        {
            _serviceRepository = serviceRepository;
            _serviceValidation = serviceValidation;
        }

        public async Task<Response<Service>> GetReponseDeleteService(string id)
        {
            try
            {
                int _id = _serviceValidation.ValidateId(id);
                if (_id < 0)
                {
                    return new Response<Service>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Service service = _serviceRepository.GetServiceById(_id);
                if (service == null)
                {
                    return new Response<Service>
                    {
                        Message = "Dịch vụ không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                
                _serviceRepository.DeleteAService(service);
                
                return new Response<Service>
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

        public async Task<Response<Service>> GetReponseServiceById(string id)
        {
            try
            {
                int _id = _serviceValidation.ValidateId(id);
                if (_id < 0)
                {
                    return new Response<Service>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Service service = _serviceRepository.GetServiceById(_id);

                if (service == null)
                {
                    return new Response<Service>
                    {
                        Message = "Dịch vụ không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<Service>
                {
                    Data = service,
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

        public async Task<Response<List<Service>>> GetReponseServices(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                List<Service> services = _serviceRepository.GetAll(page, pageSize);

                if (services == null)
                {
                    return new Response<List<Service>>
                    {
                        Message = "Danh sách dịch vụ không tồn tại!",
                        Success = false,
                        StatusCode = 200
                    };
                }

                return new Response<List<Service>>
                {
                    Data = services,
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

        public async Task<Response<Service>> GetReponseUpdateService(string id, Service service)
        {
            try
            {
                int _id = _serviceValidation.ValidateId(id);
                if (_id < 0)
                {
                    return new Response<Service>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Service _service = _serviceRepository.GetServiceById(int.Parse(id));
                if (_service == null)
                {
                    return new Response<Service>
                    {
                        Message = "Dịch vụ không tồn tại!",
                        Success = false,
                        StatusCode = 404,
                    };
                }

                service.Id = int.Parse(id);
                _serviceRepository.UpdateAService(service);

                return new Response<Service>
                {
                    Data = service,
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

        public async Task<Response<Service>> GetResponseCreateAService(Service service)
        {
            try
            {
                if (service.Id != 0)
                {
                    return new Response<Service>
                    {
                        Message = "Không cần thêm Id khi tạo 1 dịch vụ mới",
                        Success = false,
                        StatusCode = 400
                    };
                }
                
                _serviceRepository.CreateAService(service);
                return new Response<Service>
                {
                    Data = service,
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
    }
}
