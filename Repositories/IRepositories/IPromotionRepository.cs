using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IPromotionRepository
    {
        Promotion GetPromotionById(int id);
        List<Promotion> GetPromotionList(int pageIndex, int pageSize);
        List<Promotion> GetPromotionListVerMobile(int pageIndex, int pageSize);
        void CreatePromotion(Promotion promotion);
        void UpdatePromotion(Promotion promotion);
        void ChangePromotionStatus(Promotion promotion);
        int CountPromotions();
        int CountPromotionsVerMobile();
        List<Promotion> SearchPromotion(string searchString, int page, int pageSize);
        int CountPromotionWhenSearch(string searchString);
    }
}
