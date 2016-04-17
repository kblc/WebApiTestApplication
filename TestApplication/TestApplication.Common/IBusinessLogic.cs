using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApplication.Common.reports;

namespace TestApplication.Common
{
    /// <summary>
    /// Business logic implementation
    /// </summary>
    public interface IBusinessLogic : IDisposable
    {
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <param name="includeOrders">True to include orders</param>
        /// <returns>Customer array</returns>
        ICustomer[] GetCustomers(bool includeOrders);

        /// <summary>
        /// Get customer by identifier
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer. NUll if not found</returns>
        ICustomer GetCustomer(long customerId);

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <returns>Customer</returns>
        ICustomer CreateCustomer();

        /// <summary>
        /// Create new order for customer
        /// </summary>
        /// <param name="customer">Customer to add an order</param>
        /// <returns>Order</returns>
        IOrder CreateOrder(ICustomer customer);

        /// <summary>
        /// Save order to database
        /// </summary>
        /// <param name="order">Order to save</param>
        /// <returns>Saved order</returns>
        IOrder SaveOrder(IOrder order);

        /// <summary>
        /// Save customer
        /// </summary>
        /// <param name="customer">Customer to save</param>
        /// <returns>Saved customer</returns>
        ICustomer SaveCustomer(ICustomer customer);

        /// <summary>
        /// Create customer report
        /// </summary>
        /// <returns>Report</returns>
        ICustomerReport CreateCustomerReport();
    }
}
