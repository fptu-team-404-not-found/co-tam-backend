using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class WorkerTag
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int HouseWorkerId { get; set; }

        public virtual HouseWorker? HouseWorker { get; set; } = null!;
    }
}
