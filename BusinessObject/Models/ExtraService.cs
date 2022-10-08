using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Models
{
    public partial class ExtraService
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Active { get; set; }
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; } = null!;
    }
}
