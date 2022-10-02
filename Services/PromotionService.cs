using BusinessObject.Models;
using Repositories.IRepositories;
using Services.IServices;

namespace Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<Response<Promotion>> GetReponsePromotionById(int id)
        {
            Promotion promotion = _promotionRepository.GetPromotionById(id);

            if (promotion == null)
            {
                return new Response<Promotion>
                {
                    Message = "Ưu đãi không tồn tại!",
                    Success = false
                };
            }

            return new Response<Promotion>
            {
                Data = promotion,
                Message = "Thành công",
                Success = true
            };
        }
    }
}
