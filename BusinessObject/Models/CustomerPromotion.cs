using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class CustomerPromotion
    {
        public int Id { get; set; }
        public bool IsUsed { get; set; }
        public int CustomerId { get; set; }
        public int PromotionId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Promotion Promotion { get; set; } = null!;
    }
}
