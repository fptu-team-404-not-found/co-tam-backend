using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICustomerService
    {
        Task<Response<Customer>> GetReponseCustomereById(string id);
        Task<Response<List<Customer>>> GetReponseCustomers(int pageIndex, int pageSize);
        Task<Response<Customer>> GetResponseCreateACustomer(Customer customer);
        Task<Response<Customer>> GetReponseUpdateCustomer(string id, Customer customer);
        Task<Response<Customer>> GetReponseChangeStatusCustomer(string id);
        Task<Response<int>> GetResponseCustomerNumber();
        Task<Response<int>> CustomerOrder(Order order);
        Task<Response<OrderDetail>> CustomerOrderDetail(OrderDetail orderDetail, int orderId);
        Task<Response<List<Customer>>> SearchAccountCustomer(string searchString, int page, int pageSize);
        Task<Response<int>> CountCustomerWhenSearch(string searchString);
    }
}
