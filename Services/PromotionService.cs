using BusinessObject.Models;
using Repositories.IRepositories;
using Services.IServices;
using Services.ValidationHandling;

namespace Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly PromotionValidation _promotionValidation;

        public PromotionService(IPromotionRepository promotionRepository, PromotionValidation promotionValidation)
        {
            _promotionRepository = promotionRepository;
            _promotionValidation = promotionValidation;
        }

        public async Task<Response<int>> CountPromotionsVerMobile()
        {
            try
            {
                int promotionNumber = _promotionRepository.CountPromotionsVerMobile();
                return new Response<int>
                {
                    Data = promotionNumber,
                    Message = "Thành công",
                    StatusCode = 200,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<int>> CountPromotionWhenSearch(string searchString)
        {
            try
            {
                var count = _promotionRepository.CountPromotionWhenSearch(searchString);
                if (count == 0)
                {
                    return new Response<int>
                    {
                        Message = "Số lượng promotion không tồn tại",
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

        public async Task<Response<Promotion>> GetReponseChangeStatusPromotion(string id)
        {
            try
            {
                int _id = _promotionValidation.ValidateId(id);
                if (_id < 0)
                {
                    return new Response<Promotion>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Promotion promotion = _promotionRepository.GetPromotionById(_id);
                if (promotion == null)
                {
                    return new Response<Promotion>
                    {
                        Message = "Khách hàng không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                _promotionRepository.ChangePromotionStatus(promotion);

                return new Response<Promotion>
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

        public async Task<Response<Promotion>> GetReponsePromotionById(string id)
        {
            try
            {
                int _id = _promotionValidation.ValidateId(id);
                if (_id < 0)
                {
                    return new Response<Promotion>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                } 

                Promotion promotion = _promotionRepository.GetPromotionById(_id);

                if (promotion == null)
                {
                    return new Response<Promotion>
                    {
                        Message = "Ưu đãi không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<Promotion>
                {
                    Data = promotion,
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

        public async Task<Response<List<Promotion>>> GetReponsePromotions(int pageIndex, int pageSize)
        {
            try
            {
                List<Promotion> promotions = _promotionRepository.GetPromotionList(pageIndex, pageSize);

                if (promotions == null)
                {
                    return new Response<List<Promotion>>
                    {
                        Message = "Danh sách khuyến mãi không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<List<Promotion>>
                {
                    Data = promotions,
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

        public async Task<Response<List<Promotion>>> GetReponsePromotionsVerMobile(int pageIndex, int pageSize)
        {
            try
            {
                List<Promotion> promotions = _promotionRepository.GetPromotionListVerMobile(pageIndex, pageSize);

                if (promotions == null)
                {
                    return new Response<List<Promotion>>
                    {
                        Message = "Danh sách khuyến mãi không tồn tại!",
                        Success = false,
                        StatusCode = 404
                    };
                }

                return new Response<List<Promotion>>
                {
                    Data = promotions,
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

        public async Task<Response<Promotion>> GetReponseUpdatedPromotion(string id, Promotion promotion)
        {
            try
            {
                int _id = _promotionValidation.ValidateId(id);
                if (_id < 0)
                {
                    return new Response<Promotion>
                    {
                        Message = "Id không khả dụng",
                        Success = false,
                        StatusCode = 400
                    };
                }

                Promotion _promotion = _promotionRepository.GetPromotionById(int.Parse(id));
                if (_promotion == null)
                {
                    return new Response<Promotion>
                    {
                        Message = "Ưu đãi không tồn tại!",
                        Success = false,
                        StatusCode = 404,
                    };
                }

                // TODO: Validation Promotion Input - Status code = 422

                promotion.Id = int.Parse(id);
                _promotionRepository.UpdatePromotion(promotion);

                return new Response<Promotion>
                {
                    Data = promotion,
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

        public async Task<Response<Promotion>> GetResponseCreateAPromotion(Promotion promotion)
        {
            try
            {
                if (promotion.Id != 0)
                {
                    return new Response<Promotion>
                    {
                        Message = "Không cần thêm Id khi tạo 1 khuyến mãi mới",
                        Success = false,
                        StatusCode = 400
                    };
                }

                _promotionRepository.CreatePromotion(promotion);
                return new Response<Promotion>
                {
                    Data = promotion,
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

        public async Task<Response<int>> GetResponsePromotionNumber()
        {
            try
            {
                int promotionNumber = _promotionRepository.CountPromotions();
                return new Response<int>
                {
                    Data = promotionNumber,
                    Message = "Thành công",
                    StatusCode = 200,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response<List<Promotion>>> SearchPromotion(string searchString, int page, int pageSize)
        {
            try
            {
                if (page <= 1)
                {
                    page = 1;
                }
                if (string.IsNullOrEmpty(searchString))
                {
                    return new Response<List<Promotion>>
                    {
                        Message = "Hãy nhập gì đó để tìm kiếm",
                        Success = false,
                        StatusCode = 400
                    };
                }
                var lst = _promotionRepository.SearchPromotion(searchString, page, pageSize);
                if (lst == null)
                {
                    return new Response<List<Promotion>>
                    {
                        Message = "Không tìm thấy",
                        Success = false,
                        StatusCode = 400
                    };
                }
                return new Response<List<Promotion>>
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
    }
}
