using System;
using System.Collections.Generic;
using System.Linq;
using TestApplication.Common;

namespace TestApplication.BusinessLogic.dto
{
    /// <summary>
    /// Customer
    /// </summary>
    public class CustomerDto : ICustomer
    {
        /// <summary>
        /// Customer's identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Customer's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Customer's orders
        /// </summary>
        public List<OrderDto> Orders { get; set; }

        /// <summary>
        /// Customer's orders
        /// </summary>
        IEnumerable<IOrder> ICustomer.Orders
        {
            get
            {
                return Orders.Cast<IOrder>();
            }
        }
    }
}