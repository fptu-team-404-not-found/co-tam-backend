using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly cotamContext _dbContext;

        public OrderRepository(cotamContext context)
        {
            _dbContext = context;
        }
        public int CountOrder()
        {
            try
            {
                int count = _dbContext.Orders.Count();
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Order> GetAllOrderWithPagination(int page, int pageSize)
        {
            try
            {
                var list = _dbContext.Orders.Include(x => x.House).Include(x => x.Package).Include(x => x.Promotion).Include(x => x.PaymentMethod)
                       .Skip((page - 1) * (int)pageSize)
                       .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                var order = _dbContext.Orders.Include(x => x.House).Include(x => x.Package).Include(x => x.Promotion).Include(x => x.PaymentMethod).FirstOrDefault(x => x.Id == id);
                if (order != null)
                { 
                    return order;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
