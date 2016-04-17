using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TestApplication.Common;

namespace TestApplication.Models
{
    public class CustomerWidthOrders : Customer
    {
        public CustomerWidthOrders() {
            Orders = new List<Order>();
        }

        /// <summary>
        /// Customer's orders
        /// </summary>
        public List<Order> Orders { get; private set; }

        public new static CustomerWidthOrders GetByBusinessLogic(ICustomer customer)
        {
            if (customer == null)
                return null;

            var res = new CustomerWidthOrders { Email = customer.Email, Id = customer.Id, Name = customer.Name };
            if (customer.Orders != null)
            {
                foreach (var order in customer.Orders.Select(Order.GetByBusinessLogic))
                    res.Orders.Add(order);
            }
            return res;
        }
    }
}