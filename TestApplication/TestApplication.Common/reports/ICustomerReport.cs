using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TestApplication.Common.reports
{
    /// <summary>
    /// Report by customer
    /// </summary>
    public interface ICustomerReport
    {
        /// <summary>
        /// Total customer count
        /// </summary>
        [Description("Total customer count")]
        long TotalCustomerCount { get; }

        /// <summary>
        /// Total order count
        /// </summary>
        [Description("Total order count")]
        long TotalOrderCount { get; }

        /// <summary>
        /// Average order count per one customer
        /// </summary>
        [Description("Average order count per one customer")]
        decimal AvgOrdersPerCustomer { get; }

        /// <summary>
        /// Average price value
        /// </summary>
        [Description("Average price value")]
        decimal AvgPrice { get; }
    }
}
