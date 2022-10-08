using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.ValidationHandling
{
    public class AdminValidation
    {
        public string CheckCreateNewAdmin(AdminManager admin)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (string.IsNullOrEmpty(admin.Name))
                return "Hãy Nhập Name";
            if (string.IsNullOrEmpty(admin.Email))
                return "Hãy Nhập Email";
            if (!regex.IsMatch(admin.Email))
                return "Hãy Nhập Email Đúng Format";
            return "ok";
        }
    }
}
