using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.ValidationHandling
{
    public class ManagerValidation
    {
        public string CheckCreateNewManager(AdminManager manager)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (string.IsNullOrEmpty(manager.Name))
                return "Hãy Nhập Name";
            if (string.IsNullOrEmpty(manager.Email))
                return "Hãy Nhập Email";
            if (!regex.IsMatch(manager.Email))
                return "Hãy Nhập Email Đúng Format";
            return "ok";
        }
    }
}
