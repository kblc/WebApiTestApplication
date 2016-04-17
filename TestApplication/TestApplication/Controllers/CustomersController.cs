using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using TestApplication.Models;
using TestApplication.BusinessLogic;

namespace TestApplication.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        /// <summary>
        /// Get all customers without orders
        /// </summary>
        /// <remarks>GET api/customers</remarks>
        /// <returns>Customer list</returns>
        [HttpGet]
        [Route("")]
        public IEnumerable<Customer> GetCustomers()
        {
            using (var logic = Engine.Create())
            {
                return logic.GetCustomers().Select(c => Customer.GetByBusinessLogic(c)).ToArray();
            }
        }

        /// <summary>
        /// Get single customer by identifier
        /// </summary>
        /// <param name="customerId">Customer's identifier</param>
        /// <remarks>GET api/customers/5</remarks>
        /// <returns>Customer with orders</returns>
        [HttpGet]
        [Route("{customerId:int}")]
        public CustomerWidthOrders GetCustomer(int customerId)
        {
            using (var logic = Engine.Create())
            {
                return CustomerWidthOrders.GetByBusinessLogic(logic.GetCustomer(customerId));
            }
        }

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <remarks>PUT api/customers</remarks>
        /// <returns>Added customer</returns>
        [HttpPut]
        [Route("", Order = 10)]
        public Customer AddCustomer([FromBody()]Customer customer)
        {
            using (var logic = Engine.Create())
            {
                var newCustomer = logic.CreateCustomer();
                newCustomer.Email = customer.Email;
                newCustomer.Name = customer.Name;
                var savedCustomer = logic.SaveCustomer(newCustomer);
                return Customer.GetByBusinessLogic(savedCustomer);
            }
        }

        /// <summary>
        /// Add customer's order
        /// </summary>
        /// <param name="customerId">Customer's identifier</param>
        /// <param name="order">Customer's order</param>
        /// <remarks>PUT api/customers/5</remarks>
        /// <returns>Added order</returns>
        [HttpPut]
        [Route("{customerId:int}", Order = 0)]
        [Route("{customerId:int}/orders")]
        public Order AddOrder(int customerId, [FromBody]Order order)
        {
            using (var logic = Engine.Create())
            {
                var customer = logic.GetCustomer(customerId);
                if (customer != null)
                {
                    var newOrder = logic.CreateOrder(customer);
                    newOrder.Price = order.Price;
                    var savedOrder = logic.SaveOrder(newOrder);
                    return Order.GetByBusinessLogic(savedOrder);
                }

                return null;
            }
        }

        /// <summary>
        /// Get customer's orders by customer identifier
        /// </summary>
        /// <param name="customerId"></param>
        /// <remarks>GET api/customers/5/orders</remarks>
        /// <returns>Orders for customer</returns>
        [HttpGet]
        [Route("{customerId:int}/orders")]
        public IEnumerable<Order> GetCustomerOrders(int customerId)
        {
            using (var logic = Engine.Create())
            {
                var customer = logic.GetCustomer(customerId);
                if (customer != null && customer.Orders != null)
                {
                    return customer.Orders.Select(Order.GetByBusinessLogic).ToArray();
                }

                return null;
            }
        }
    }
}