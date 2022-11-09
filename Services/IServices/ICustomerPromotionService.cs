using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICustomerPromotionService
    {
        Task<Response<List<CustomerPromotion>>> GetAllCustomerPromotionWithPagination(int cusId, int page, int pageSize);
        Task<Response<string>> CountCustomerPromotion(int cusId);
        Task<Response<string>> DisableCustomerPromotions(int cusId);
        Task<Response<string>> CreateNewCustomerPromotion(CustomerPromotion customerPromotion);
        Task<Response<CustomerPromotion>> GetCustomerPromotionById(int cusId);
        Task<Response<List<CustomerPromotion>>> GetCustomerPromotionsNotUseByCusId(int cusId);
    }
}
