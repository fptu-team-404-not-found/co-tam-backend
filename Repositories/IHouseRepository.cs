using BusinessObject.Models;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IHouseRepository
    {
        House GetHouseById(int id);
        
    }
}
