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