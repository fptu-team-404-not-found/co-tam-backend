using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class AdminManager
    {
        public AdminManager()
        {
            HouseWorkers = new HashSet<HouseWorker>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? LinkFacebook { get; set; }
        public string? Avatar { get; set; }
        public bool? Active { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<HouseWorker> HouseWorkers { get; set; }
    }
}
