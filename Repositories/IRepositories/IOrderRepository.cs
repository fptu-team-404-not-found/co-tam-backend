using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrderWithPagination(int page, int pageSize);
        int CountOrder();
        Order GetOrderById(int id);
    }
}
