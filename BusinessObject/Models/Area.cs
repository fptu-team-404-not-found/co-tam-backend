using System;
using System.Collections.Generic;

namespace BusinessObject.Models
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
        public string? District { get; set; }
        public string? City { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }
    }
}
