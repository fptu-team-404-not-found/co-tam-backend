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
    }
}
