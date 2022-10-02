using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ValidationHandling
{
    public class ValidationAdminManager
    {
        public string ValidateId(string email)
        {
            if (string.IsNullOrEmpty(email)) {
                return "Khoong dc trong";
            }
            return "ok";
        }
    }
}
