using System;
using System.Collections.Generic;

namespace CoTamApp.Models
{
    public partial class District
    {
        public District()
        {
            Areas = new HashSet<Area>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CityId { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual ICollection<Area> Areas { get; set; }
    }
}
