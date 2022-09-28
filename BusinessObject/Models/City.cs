using System;
using System.Collections.Generic;

namespace CoTamApp.Models
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<District> Districts { get; set; }
    }
}
