using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class WorkerInOrder
    {
        public int Id { get; set; }
        public int HouseWorkerId { get; set; }
        public int OrderId { get; set; }
        public int? Rating { get; set; }

        public virtual HouseWorker HouseWorker { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
