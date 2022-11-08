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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly cotamContext _cotamContext;

        public CustomerRepository(cotamContext cotamContext)
        {
            _cotamContext = cotamContext;
        }
        public void CreateCustomer(Customer customer)
        {
            try
            {
                _cotamContext.Customers.Add(customer);
                _cotamContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void CustomerOrderDetail(OrderDetail orderDetail, int orderId)
        {
            try
            {
                var order = _cotamContext.Orders.FirstOrDefault(x => x.Id == orderId);
                if (order != null)
                {
                    orderDetail.OrderId = orderId;
                    _cotamContext.OrderDetails.Add(orderDetail);
                    _cotamContext.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void CustomerOrder(Order order)
        {
            try
            {
                OrderStates datDonThanhCong = OrderStates.DAT_DON_THANH_CONG;
                order.DateTime = DateTime.Now;
                if (order.PaymentMethodId == 2)
                {
                    House house = _cotamContext.Houses.FirstOrDefault(h => h.Id == order.HouseId);
                    Customer customer = _cotamContext.Customers.FirstOrDefault(c => c.Id == house.CustomerId);

                    if (order.Total <= customer.EWallet)
                    {
                        customer.EWallet -= order.Total;
                        _cotamContext.Customers.Update(customer);
                    } else
                    {
                        throw new Exception("Số tiền trong ví không còn đủ để thực hiện giao dịch!");
                    }
                }
                order.PaymentMethodId = 1;
                order.OrderState = Array.IndexOf(Enum.GetValues(datDonThanhCong.GetType()), datDonThanhCong);
                _cotamContext.Orders.Add(order);
                _cotamContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void ChangeCustomerStatus(Customer customer)
        {
            try
            {
                if (customer.Active == false)
                {
                    customer.Active = true;
                } else
                {
                    customer.Active = false;
                }
                _cotamContext.Customers.Update(customer);
                _cotamContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = null;
            try
            {
                customer = _cotamContext.Customers.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customer;
        }

        public List<Customer> GetCustomerList(int pageIndex, int pageSize)
        {
            var list = _cotamContext.Customers
                        .Skip((pageIndex - 1) * (int)pageSize)
                        .Take((int)pageSize).ToList();
            return list;
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                Customer oldCustomer = _cotamContext.Customers.FirstOrDefault(c => c.Id == customer.Id);
                if (customer.LinkFacebook == null && oldCustomer.LinkFacebook != null)
                {
                    customer.LinkFacebook = oldCustomer.LinkFacebook;
                }
                if (customer.Avatar == null && oldCustomer.Avatar != null)
                {
                    customer.Avatar = oldCustomer.Avatar;
                }
                if (customer.EWallet == null && oldCustomer.EWallet != null)
                {
                    customer.EWallet = oldCustomer.EWallet;
                }
                if (customer.Active == null && oldCustomer.Active != null)
                {
                    customer.Active = oldCustomer.Active;
                }
                if (customer.CustomerPromotions == null && oldCustomer.CustomerPromotions != null)
                {
                    customer.CustomerPromotions = oldCustomer.CustomerPromotions;
                }
                if (customer.Houses == null && oldCustomer.Houses != null)
                {
                    customer.Houses = oldCustomer.Houses;
                }
                _cotamContext.ChangeTracker.Clear();
                _cotamContext.Entry(customer).State = EntityState.Modified;
                _cotamContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountCustomers()
        {
            try
            {
                return _cotamContext.Customers.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
