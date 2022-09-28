using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public void Logout()
        {
            AccessToken = null;
            RefreshToken = null;

        }
    }
}
