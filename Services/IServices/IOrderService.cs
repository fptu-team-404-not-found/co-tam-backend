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
        Task<Response<string>> ChangeTheOrderState(int orderId);
        Task<Response<string>> ChangeTheOrderStateToCancle(int orderId);
        Task<Response<List<Order>>> GetOrdersHistoryByCusId(int cusId);
        Task<Response<List<Order>>> GetOrdersHasStateDangDatByCusId(int cusId);
        Task<Response<List<Order>>> SearchOrder(string searchString, int page, int pageSize);
        Task<Response<int>> CountOrdersWhenSearch(string searchString);
    }
}
