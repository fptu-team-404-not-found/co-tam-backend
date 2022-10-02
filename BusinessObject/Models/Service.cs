using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Service
    {
        public Service()
        {
            ExtraServices = new HashSet<ExtraService>();
            Packages = new HashSet<Package>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Active { get; set; }

        public virtual ICollection<ExtraService> ExtraServices { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}
