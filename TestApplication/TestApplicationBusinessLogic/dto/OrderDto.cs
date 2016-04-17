using System;
using TestApplication.Common;

namespace TestApplication.BusinessLogic.dto.Models
{
    /// <summary>
    /// Customer order
    /// </summary>
    public class OrderDto : IOrder
    {
        public OrderDto() {
            CreatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Customer identifier
        /// </summary>
        public long CustomerId { get; set; }

        /// <summary>
        /// Order's identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Order's creation date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Order's price
        /// </summary>
        public decimal Price { get; set; }
    }
}