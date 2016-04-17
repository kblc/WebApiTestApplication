using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TestApplication.Common.reports;

namespace TestApplication.Models
{
    /// <summary>
    /// Report by customer
    /// </summary>
    public class CustomerReport
    {
        /// <summary>
        /// Total customer count
        /// </summary>
        [Description("Total customer count")]
        public long TotalCustomerCount { get; set; }

        /// <summary>
        /// Total order count
        /// </summary>
        [Description("Total order count")]
        public long TotalOrderCount { get; set; }

        /// <summary>
        /// Average order count per one customer
        /// </summary>
        [Description("Average order count per one customer")]
        public decimal AvgOrdersPerCustomer { get; set; }

        /// <summary>
        /// Average price value
        /// </summary>
        [Description("Average price value")]
        public decimal AvgPrice { get; set; }

        public static CustomerReport GetByBusinessLogic(ICustomerReport report)
        {
            if (report == null)
                return null;

            return new CustomerReport { AvgOrdersPerCustomer = report.AvgOrdersPerCustomer, AvgPrice = report.AvgPrice, TotalCustomerCount = report.TotalCustomerCount, TotalOrderCount = report.TotalOrderCount };
        }
    }
}