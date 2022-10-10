using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class HouseWorker
    {
        public HouseWorker()
        {
            WorkerInOrders = new HashSet<WorkerInOrder>();
            WorkerTags = new HashSet<WorkerTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? LinkFacebook { get; set; }
        public string? Avatar { get; set; }
        public int Active { get; set; }
        public int AreaId { get; set; }
        public int ManagerId { get; set; }

        public virtual AdminManager? Manager { get; set; } = null!;
        public virtual ICollection<WorkerInOrder> WorkerInOrders { get; set; }
        public virtual ICollection<WorkerTag> WorkerTags { get; set; }
    }
}
