using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationHandling
{
    public class CustomerValidation
    {
        public int ValidationId(string id)
        {
            try
            {
                int _id = int.Parse(id);
                return _id;
            }
            catch (FormatException)
            {
                return -1;
            }
        }
    }
}
