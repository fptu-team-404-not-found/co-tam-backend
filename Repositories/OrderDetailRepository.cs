﻿using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly cotamContext _dbContext;

        public OrderDetailRepository(cotamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            try
            {
                var od = _dbContext.OrderDetails.Include(x => x.Order).Include(x => x.ExtraService).FirstOrDefault(x => x.Id == id);
                if (od != null)
                {
                    return od;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderId)
        {
            try
            {
                var ods = _dbContext.OrderDetails
                    .Include(x => x.Order)
                    .Include(x => x.ExtraService)
                    .Where(x => x.OrderId == orderId)
                    .ToList();
                if (ods != null)
                {
                    return ods;
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
