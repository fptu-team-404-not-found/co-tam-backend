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
    public class InformationService : IInformationService
    {
        private readonly IInformationRepository _informationRepository;
        private readonly InformationValidation _informationValidation;

        public InformationService(IInformationRepository informationRepository, InformationValidation informationValidation)
        {
            _informationRepository = informationRepository;
            _informationValidation = informationValidation;
        }
        public async Task<Response<string>> CountInformation()
        {
            try
            {
                var count = _informationRepository.CountInformation();
                if (count == 0)
                {
                    return new Response<string>
                    {
                        Message = "Số lượng information không tồn tại",
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

        public async Task<Response<string>> CreateNewInformation(Information infor)
        {
            try
            {
                var validate = _informationValidation.CheckInformationValidation(infor);
                if (validate != "ok")
                {
                    return new Response<string> {
                        Message = validate,
                        Success = false,
                        StatusCode = 400
                    };
                }
                infor.Active = true;
                _informationRepository.CreateNewInformation(infor);
                return new Response<string>
                {
                    Data = infor.Id.ToString(),
                    Message = "Thành Công",
                    Success = true,
                    StatusCode = 201
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<string>> DisableOrEnableInformation(int inforId)
        {
            try
            {
                var result = _informationRepository.DisableOrEnableInformation(inforId);
                if (result)
                {
                    return new Response<string>
                    {
                        Message = "Đã Thực Hiện Thành Công Thao Tác Disable/Enable Information",
                        Success = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    return new Response<string>
                    {
                        Message = "Disable/Enable Information Thất Bại",
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

        public async Task<Response<List<Information>>> GetAllInformationWithPagination(int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                var lst = _informationRepository.GetAllInformationWithPagination(page, pageSize);
                return new Response<List<Information>>
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

        public async Task<Response<Information>> GetInformationById(int id)
        {
            try
            {
                var infor = _informationRepository.GetInformationById(id);
                if (infor != null)
                {
                    return new Response<Information>
                    {
                        Data = infor,
                        Message = "Thành Công",
                        Success = true
                    };
                }
                else
                {
                    return new Response<Information>
                    {
                        Message = "Không tìm thấy Information có id là " + id,
                        Success = false
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<Information>> UpdateInformation(Information information)
        {
            try
            {
                if (information.Id < 0)
                {
                    return new Response<Information>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var validate = _informationValidation.CheckInformationValidation(information);
                if (validate != "ok")
                {
                    return new Response<Information>
                    {
                        Message = validate,
                        Success = false,
                        StatusCode = 400
                    };
                }

                var inforExist = _informationRepository.GetInformationById(information.Id);
                if (inforExist == null)
                {
                    return new Response<Information>
                    {
                        Message = "Information không tồn tại!",
                        Success = false,
                        StatusCode = 404,
                    };
                }
                inforExist.Name = information.Name;
                inforExist.Discription = information.Discription;
                _informationRepository.UpdateInformation(inforExist);

                return new Response<Information>
                {
                    Data = inforExist,
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
