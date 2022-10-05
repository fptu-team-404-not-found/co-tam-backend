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

        public async Task<Response<List<Service>>> GetReponseServices()
        {
            try
            {
                List<Service> services = _serviceRepository.GetAll();

                if (services == null)
                {
                    return new Response<List<Service>>
                    {
                        Message = "Danh sách dịch vụ không tồn tại!",
                        Success = false,
                        StatusCode = 404
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

        public async Task<Response<Service>> GetResponseCreateAService(Service service)
        {
            try
            {
                /// <response code="422">Validation exception</response>
                
                _serviceRepository.CreatAService(service);
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
