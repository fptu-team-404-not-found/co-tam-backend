using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.ValidationHandling
{
    public class HouseWorkerValidation
    {
        public string CheckCreateNewHouseWorker(HouseWorker houseWorker)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (string.IsNullOrEmpty(houseWorker.Name))
                return "Hãy Nhập Name";
            if (string.IsNullOrEmpty(houseWorker.Email))
                return "Hãy Nhập Email";
            if (!regex.IsMatch(houseWorker.Email))
                return "Hãy Nhập Email Đúng Format";
            return "ok";
        }
    }
}
