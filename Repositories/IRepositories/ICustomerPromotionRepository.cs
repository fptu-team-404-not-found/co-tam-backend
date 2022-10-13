using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface ICustomerPromotionRepository
    {
        List<CustomerPromotion> GetAllCustomerPromotionWithPagination(int cusId, int page, int pageSize);
        int CountCustomerPromotion(int cusId);
        bool DisableOrEnableCustomerPromotions(int cusId);
        void CreateNewCustomerPromotion(CustomerPromotion customerPromotion);
        CustomerPromotion GetCustomerPromotionById(int cusId);
    }
}
