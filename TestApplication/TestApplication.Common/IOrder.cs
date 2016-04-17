using System;
using System.ComponentModel;

namespace TestApplication.Common
{
    /// <summary>
    /// Order
    /// </summary>
    [Description("CUstomer order data transfer object")]
    public interface IOrder
    {
        /// <summary>
        /// Order's identifier
        /// </summary>
        [Description("Order's identifier")]
        long Id { get; }

        /// <summary>
        /// Customer's identifier
        /// </summary>
        [Description("Customer's identifier")]
        long CustomerId { get; }

        /// <summary>
        /// Order's creation date
        /// </summary>
        [Description("Order's creation date")]
        DateTime CreatedDate { get; }

        /// <summary>
        /// Order's price
        /// </summary>
        [Description("Order's price")]
        decimal Price { get; set; }
    }
}
