using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace TestApplication.Repository.dbo
{
    /// <summary>
    /// Customer
    /// </summary>
    [Table("customer")]
    public class CustomerDbo
    {
        public CustomerDbo()
        {
            Orders = new List<OrderDbo>();
        }

        /// <summary>
        /// Customer's identifier
        /// </summary>
        [Key, Column("customer_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Customer's name
        /// </summary>
        [Column("name"), Required]
        public string Name { get; set; }

        /// <summary>
        /// Customer's email
        /// </summary>
        [Column("email"), Required]
        public string Email { get; set; }

        /// <summary>
        /// Customer's orders
        /// </summary>
        public virtual ICollection<OrderDbo> Orders { get; }
    }
}
