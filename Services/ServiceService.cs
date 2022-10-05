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
    }
}
