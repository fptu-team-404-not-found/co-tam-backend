using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool? IsUsed { get; set; }
        public bool? IsRevoked { get; set; }
        public DateTime? IssuedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public string? RtokenId { get; set; }
    }
}
