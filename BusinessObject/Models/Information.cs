using System;
using System.Collections.Generic;

namespace CoTamApp.Models
{
    public partial class Information
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Discription { get; set; }
        public bool? Active { get; set; }
    }
}
