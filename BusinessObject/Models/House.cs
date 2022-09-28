using System;
using System.Collections.Generic;

namespace CoTamApp.Models
{
    public partial class House
    {
        public House()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public bool? Active { get; set; }
        public int CustomerId { get; set; }
        public int BuildingId { get; set; }

        public virtual Building Building { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
