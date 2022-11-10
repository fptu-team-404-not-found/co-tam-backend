using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly cotamContext _cotamContext;

        public PromotionRepository(cotamContext cotamContext)
        {
            _cotamContext = cotamContext;
        }

        public void ChangePromotionStatus(Promotion promotion)
        {
            try
            {
                if (promotion.Active == false)
                {
                    promotion.Active = true;
                }
                else
                {
                    promotion.Active = false;
                }
                _cotamContext.Promotions.Update(promotion);
                _cotamContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountPromotions()
        {
            try
            {
                return _cotamContext.Promotions.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int CountPromotionsVerMobile()
        {
            try
            {
                return _cotamContext.Promotions.Count(x => x.Active == true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CreatePromotion(Promotion promotion)
        {
            try
            {
                promotion.Image = "https://luatvietphong.vn/wp-content/uploads/2021/08/4899545.jpg";
                _cotamContext.Promotions.Add(promotion);
                _cotamContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Promotion GetPromotionById(int id)
        {
            Promotion promotion = null;
            try
            {
                promotion = _cotamContext.Promotions.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return promotion;
        }

        public List<Promotion> GetPromotionList(int pageIndex, int pageSize)
        {
            var list = _cotamContext.Promotions
                        .Skip((pageIndex - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
            return list;
        }
        public List<Promotion> GetPromotionListVerMobile(int pageIndex, int pageSize)
        {
            var list = _cotamContext.Promotions.Where(x => x.Active == true)
                        .Skip((pageIndex - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
            return list;
        }
        public void UpdatePromotion(Promotion promotion)
        {
            try
            {
                _cotamContext.ChangeTracker.Clear();
                _cotamContext.Entry<Promotion>(promotion).State = EntityState.Modified;
                _cotamContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Promotion> SearchPromotion(string searchString, int page, int pageSize)
        {
            try
            {
                var list = _cotamContext.Promotions
                        .Where(x => x.Code.Contains(searchString)
                        || x.Description.Contains(searchString)).Skip((page - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
                if (list != null)
                {
                    return list;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public int CountPromotionWhenSearch(string searchString)
        {
            var count = _cotamContext.Promotions
                        .Where(x => x.Code.Contains(searchString)
                        || x.Description.Contains(searchString)).Count();
            return count;
        }
    }
}
