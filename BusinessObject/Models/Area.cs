using System;
using System.Collections.Generic;

namespace CoTamApp.Models
{
    public partial class Area
    {
        public Area()
        {
            Buildings = new HashSet<Building>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }
        public int DistrictId { get; set; }

        public virtual District District { get; set; } = null!;
        public virtual ICollection<Building> Buildings { get; set; }
    }
}
