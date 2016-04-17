using System.Collections.Generic;
using System.ComponentModel;

namespace TestApplication.Common
{
    /// <summary>
    /// Customer
    /// </summary>
    [Description("Customer")]
    public interface ICustomer
    {
        /// <summary>
        /// Customer's identifier
        /// </summary>
        [Description("Customer's identifier")]
        long Id { get; set; }

        /// <summary>
        /// Customer's name
        /// </summary>
        [Description("Customer's name")]
        string Name { get; set; }

        /// <summary>
        /// Customer's email
        /// </summary>
        [Description("Customer's email")]
        string Email { get; set; }

        /// <summary>
        /// Customer's orders
        /// </summary>
        [Description("Customer's orders")]
        IEnumerable<IOrder> Orders { get; }
    }
}
