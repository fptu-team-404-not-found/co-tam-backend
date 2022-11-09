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

        public string CreateNewCustomerPromotion(CustomerPromotion customerPromotion)
        {
            try
            {
                var decreasePromotion = _dbContext.Promotions.FirstOrDefault(x => x.Id == customerPromotion.PromotionId);
                if (decreasePromotion != null)
                {
                    if (decreasePromotion.Amount < 1)
                    {
                        return "Số lượng voucher đã hết";
                    }
                    else 
                    {
                        decreasePromotion.Amount -= 1;
                        _dbContext.SaveChanges();
                        _dbContext.CustomerPromotions.Add(customerPromotion);
                        _dbContext.SaveChanges();
                        if (decreasePromotion.Amount == 0)
                        {
                            decreasePromotion.Active = false;
                            _dbContext.SaveChanges();
                        }
                        return "ok";
                    }
                    
                }
                return "ok";
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
                    if (customerPromotions.IsUsed == false)
                    {
                        customerPromotions.IsUsed = true;
                        _dbContext.SaveChanges();
                        /*var promotion = _dbContext.Promotions.FirstOrDefault(x => x.Id == customerPromotions.PromotionId);
                        if (promotion != null)
                        {
                            promotion.Amount -= 1;
                            _dbContext.SaveChanges();
                            return true;
                        }*/
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckUsedForThePromotion(int cusId, int? proId)
        {
            try
            {
                var customerPromotions = _dbContext.CustomerPromotions.FirstOrDefault(x => x.CustomerId == cusId && x.PromotionId == proId);
                if (customerPromotions != null)
                {
                    if (customerPromotions.IsUsed == false)
                    {
                        customerPromotions.IsUsed = true;
                        _dbContext.SaveChanges();
                        return true;
                        /*var promotion = _dbContext.Promotions.FirstOrDefault(x => x.Id == customerPromotions.PromotionId);
                        if (promotion != null)
                        {
                            promotion.Amount -= 1;
                            _dbContext.SaveChanges();
                            return true;
                        }*/
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
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
        public bool CheckCustomerHasThisPromotion(int cusId, int? proId)
        {
            try
            {
                var cip = _dbContext.CustomerPromotions.FirstOrDefault(x => x.CustomerId == cusId && x.PromotionId == proId);
                if (cip != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<CustomerPromotion> GetCustomerPromotionsNotUseByCusId(int cusId)
        {
            try
            {
                var cusPro = _dbContext.CustomerPromotions
                    .Include(x => x.Customer)
                    .Include(x => x.Promotion)
                    .Where(x => x.CustomerId == cusId && x.IsUsed == false)
                    .ToList();
                if (cusPro != null)
                {
                    return cusPro;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
