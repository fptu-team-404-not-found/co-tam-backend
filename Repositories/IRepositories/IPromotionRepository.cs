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
        void CreatePromotion(Promotion promotion);
        void UpdatePromotion(Promotion promotion);
        void ChangePromotionStatus(Promotion promotion);
        int CountPromotions();
    }
}
