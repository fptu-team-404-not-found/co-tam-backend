using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Building
    {
        public Building()
        {
            Houses = new HashSet<House>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }
        public int AreaId { get; set; }

        public virtual Area Area { get; set; } = null!;
        public virtual ICollection<House> Houses { get; set; }
    }
}
