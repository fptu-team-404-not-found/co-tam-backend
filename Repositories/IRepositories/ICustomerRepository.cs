using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int id);

        List<Customer> GetCustomerList(int pageIndex, int pageSize);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void ChangeCustomerStatus(Customer customer);
        int CountCustomers();
        void CustomerOrder(Order order);
        void CustomerOrderDetail(OrderDetail orderDetail, int orderId);
        List<Customer> SearchAccountCustomer(string searchString, int page, int pageSize);
        int CountCustomerWhenSearch(string searchString);
    }
}
