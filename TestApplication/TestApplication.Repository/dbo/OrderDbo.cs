using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TestApplication.Repository.dbo
{
    /// <summary>
    /// Order
    /// </summary>
    [Table("order")]
    public class OrderDbo
    {
        /// <summary>
        /// Order's identifier
        /// </summary>
        [Key, Column("order_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Customer's identifier
        /// </summary>
        [ForeignKey("Customer"), Column("customer_id"), Required]
        public long CustomerId { get; set; }

        /// <summary>
        /// Customer
        /// </summary>
        public virtual CustomerDbo Customer { get; set; }

        /// <summary>
        /// Order's creation date
        /// </summary>
        [Column("created"), Required]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Order's price
        /// </summary>
        [Column("price"), Required]
        public decimal Price { get; set; }
    }
}
