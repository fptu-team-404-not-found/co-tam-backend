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
    }
}
