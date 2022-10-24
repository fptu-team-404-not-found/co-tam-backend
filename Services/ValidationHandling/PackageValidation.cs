using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationHandling
{
    public class PackageValidation
    {
        public string CheckCreateNewPackageValidation(Package package)
        {
            if (string.IsNullOrEmpty(package.Name.ToString()) || string.IsNullOrEmpty(package.Duration.ToString())
                || string.IsNullOrEmpty(package.ServiceId.ToString()))
                return "Hãy nhập số lượng người của gói , hoặc thời gian của gói , hoặc serviceId";
            return "ok";
        }
    }
}
