using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApplication.Common.reports;

namespace TestApplication.BusinessLogic.dto.report
{
    /// <summary>
    /// Report by customer
    /// </summary>
    public class CustomerReportDto : ICustomerReport
    {
        /// <summary>
        /// Average order count per one customer
        /// </summary>
        public decimal AvgOrdersPerCustomer { get { return TotalCustomerCount == 0 ? 0 : TotalOrderCount / TotalCustomerCount; } }

        /// <summary>
        /// Average price value
        /// </summary>
        public decimal AvgPrice { get; set; }

        /// <summary>
        /// Total customer count
        /// </summary>
        public long TotalCustomerCount { get; set; }

        /// <summary>
        /// Total order count
        /// </summary>
        public long TotalOrderCount { get; set; }
    }
}
