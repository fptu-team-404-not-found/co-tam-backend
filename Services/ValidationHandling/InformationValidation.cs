using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationHandling
{
    public class InformationValidation
    {
        public string CheckInformationValidation(Information information)
        {
            if (string.IsNullOrEmpty(information.Name) || string.IsNullOrEmpty(information.Discription))
                return "Hãy nhập name, description của information";
            return "ok";
        }
    }
}
