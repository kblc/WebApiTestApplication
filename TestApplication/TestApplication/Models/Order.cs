using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TestApplication.Common;

namespace TestApplication.Models
{
    public class Order
    {
        /// <summary>
        /// Order's identifier
        /// </summary>
        [Description("Order's identifier")]
        public long Id { get; set; }

        /// <summary>
        /// Order's creation date
        /// </summary>
        [Description("Order's creation date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Order's price
        /// </summary>
        [Description("Order's price")]
        public decimal Price { get; set; }

        public static Order GetByBusinessLogic(IOrder order)
        {
            if (order == null)
                return null;

            return new Order { Id = order.Id, Price = order.Price, CreatedDate = order.CreatedDate };
        }
    }
}