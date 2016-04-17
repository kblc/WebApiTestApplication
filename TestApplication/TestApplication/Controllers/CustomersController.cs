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
        // GET api/customers
        [HttpGet]
        [Route("")]
        public IEnumerable<Customer> GetCustomers()
        {
            using (var logic = Engine.Create())
            {
                return logic.GetCustomers().Select(c => Customer.GetByBusinessLogic(c)).ToArray();
            }
        }

        // GET api/customers/5
        [HttpGet]
        [Route("{customerId:int}")]
        public CustomerWidthOrders GetCustomer(int customerId)
        {
            using (var logic = Engine.Create())
            {
                return CustomerWidthOrders.GetByBusinessLogic(logic.GetCustomer(customerId));
            }
        }

        // PUT api/customers
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

        // PUT api/customers/5
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

        // GET api/customers/5/orders
        [HttpGet]
        [Route("{customerId:int}/orders")]
        public IEnumerable<Order> GetOrdersOrder(int customerId)
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