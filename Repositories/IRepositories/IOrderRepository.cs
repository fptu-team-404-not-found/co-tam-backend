﻿using BusinessObject.Models;
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
        void ChangeTheOrderState(int orderId);
        void ChangeTheOrderStateToCancle(int orderId);
        List<Order> GetOrdersHistoryByCusId(int houseId);
        List<Order> GetOrdersHasStateDangDatByCusId(int houseId);
        List<Order> SearchOrder(string searchString, int page, int pageSize);
        int CountOrdersWhenSearch(string searchString);
    }
}
