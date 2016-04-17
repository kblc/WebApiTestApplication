using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using TestApplication.Common;

namespace TestApplication.Models
{
    public class Customer
    {
        /// <summary>
        /// Customer's identifier
        /// </summary>
        [Description("Customer's identifier")]
        public long Id { get; set; }

        /// <summary>
        /// Customer's name
        /// </summary>
        [Description("Customer's name")]
        public string Name { get; set; }

        /// <summary>
        /// Customer's email
        /// </summary>
        [Description("Customer's email")]
        public string Email { get; set; }

        public static Customer GetByBusinessLogic(ICustomer customer)
        {
            if (customer == null)
                return null;
            return new Customer { Email = customer.Email, Id = customer.Id, Name = customer.Name };
        }
    }
}