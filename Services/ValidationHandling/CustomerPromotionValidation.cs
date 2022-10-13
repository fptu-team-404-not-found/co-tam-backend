using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationHandling
{
    public class CustomerPromotionValidation
    {
        public string CheckCreateNewCustomerValidation(CustomerPromotion customerPromotion)
        {
            if (string.IsNullOrEmpty(customerPromotion.CustomerId.ToString()) || string.IsNullOrEmpty(customerPromotion.PromotionId.ToString()))
                return "Hãy nhập đủ customerId và promotionId";
            return "ok";
        }
    }
}
