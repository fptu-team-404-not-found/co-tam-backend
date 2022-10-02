using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            CustomerPromotions = new HashSet<CustomerPromotion>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Value { get; set; }
        public double? Discount { get; set; }
        public int? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<CustomerPromotion> CustomerPromotions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
