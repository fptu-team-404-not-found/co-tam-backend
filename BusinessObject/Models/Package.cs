using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Package
    {
        public Package()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public int Active { get; set; }
        public int ServiceId { get; set; }
        public decimal? Price { get; set; }

        public virtual Service? Service { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
