﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetOrderDetailsByOrderId(int orderId);
        OrderDetail GetOrderDetailById(int id);
    }
}
