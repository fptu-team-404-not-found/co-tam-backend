using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CoTamApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerPromotions = new HashSet<CustomerPromotion>();
            Houses = new HashSet<House>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? LinkFacebook { get; set; }
        public string? Avatar { get; set; }
        public decimal EWallet { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<CustomerPromotion> CustomerPromotions { get; set; }
        public virtual ICollection<House> Houses { get; set; }
    }
}
