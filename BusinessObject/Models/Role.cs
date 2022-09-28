using System;
using System.Collections.Generic;

namespace CoTamApp.Models
{
    public partial class Role
    {
        public Role()
        {
            AdminManagers = new HashSet<AdminManager>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AdminManager> AdminManagers { get; set; }
    }
}
