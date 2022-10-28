using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            WorkerInOrders = new HashSet<WorkerInOrder>();
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public int HouseId { get; set; }
        public int PackageId { get; set; }
        public int? PromotionId { get; set; }
        public int PaymentMethodId { get; set; }
        public int OrderState { get; set; }

        public virtual House? House { get; set; } = null!;
        public virtual Package? Package { get; set; } = null!;
        public virtual PaymentMethod? PaymentMethod { get; set; } = null!;
        public virtual Promotion? Promotion { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<WorkerInOrder> WorkerInOrders { get; set; }
    }
}
