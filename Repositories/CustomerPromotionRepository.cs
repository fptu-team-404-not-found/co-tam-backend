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
    public class CustomerPromotionRepository : ICustomerPromotionRepository
    {
        private readonly cotamContext _dbContext;

        public CustomerPromotionRepository(cotamContext context)
        {
            _dbContext = context;
        }
        public int CountCustomerPromotion(int cusId)
        {
            try
            {
                var count = _dbContext.CustomerPromotions.Where(x => x.CustomerId == cusId).Count();
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateNewCustomerPromotion(CustomerPromotion customerPromotion)
        {
            try
            {
                _dbContext.CustomerPromotions.Add(customerPromotion);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DisableOrEnableCustomerPromotions(int cusId)
        {
            try
            {
                var customerPromotions = _dbContext.CustomerPromotions.FirstOrDefault(x => x.CustomerId == cusId);
                if (customerPromotions != null)
                {
                    if (customerPromotions.IsUsed == true)
                    {
                        customerPromotions.IsUsed = false;
                        _dbContext.SaveChanges();
                        var promotion = _dbContext.Promotions.FirstOrDefault(x => x.Id == customerPromotions.PromotionId);
                        if (promotion != null)
                        {
                            promotion.Amount -= 1;
                            _dbContext.SaveChanges();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public List<CustomerPromotion> GetAllCustomerPromotionWithPagination(int cusId, int page, int pageSize)
        {
            try
            {
                var list = _dbContext.CustomerPromotions.Include(x => x.Customer).Include(x => x.Promotion).Where(x => x.CustomerId == cusId)
                       .Skip((page - 1) * (int)pageSize)
                       .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CustomerPromotion GetCustomerPromotionById(int cusId)
        {
            try
            {
                var cusPro = _dbContext.CustomerPromotions.Include(x => x.Customer).Include(x => x.Promotion).FirstOrDefault(x => x.CustomerId == cusId);
                return cusPro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
