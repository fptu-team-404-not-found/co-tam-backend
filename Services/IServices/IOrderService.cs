﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IOrderService
    {
        Task<Response<List<Order>>> GetAllOrderWithPagination(int page, int pageSize);
        Task<Response<int>> CountOrder();
        Task<Response<Order>> GetOrderById(int id);
    }
}