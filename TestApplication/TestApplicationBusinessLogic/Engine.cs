using System;
using System.Collections.Generic;
using System.Linq;
using TestApplication.Common;
using TestApplication.Common.reports;
using TestApplication.BusinessLogic.dto.Models;

namespace TestApplication.BusinessLogic
{
    public class Engine : IBusinessLogic
    {
        private static List<CustomerDto> FakeCustomerDb = new List<CustomerDto>();
        private static long idCustomer = 0;
        private static long idOrder = 0;

        static Engine()
        {
            var customer0 = new CustomerDto() { Id = ++idCustomer, Email = "customer0@domain.com", Name = "customer0" }; 
            customer0.Orders = new List<OrderDto>(new[] {
                new OrderDto() { Id = ++idOrder, Price = 0.123m },
                new OrderDto() { Id = ++idOrder, Price = 0.321m }
            });
            FakeCustomerDb.Add(customer0);

            var customer1 = new CustomerDto() { Id = ++idCustomer, Email = "customer1@domain.com", Name = "customer1" };
            customer1.Orders = new List<OrderDto>(new[] {
                new OrderDto() { Id = ++idOrder, Price = 0.456m },
                new OrderDto() { Id = ++idOrder, Price = 0.654m }
            });
            FakeCustomerDb.Add(customer1);
        }

        /// <summary>
        /// User can't create business logic object
        /// </summary>
        private Engine() {}

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <param name="includeOrders">True to include orders</param>
        /// <returns>Customer array</returns>
        public ICustomer[] GetCustomers(bool includeOrders)
        {
            return FakeCustomerDb.Select(i => ConvertToCustomerDto(i, includeOrders)).ToArray();
        }

        /// <summary>
        /// Get customer by identifier
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer. NUll if not found</returns>
        public ICustomer GetCustomer(long customerId)
        {
            var foundedCustomer = FakeCustomerDb.FirstOrDefault(c => c.Id == customerId);
            return (foundedCustomer == null)
                ? null
                : ConvertToCustomerDto(foundedCustomer, true);
        }


        /// <summary>
        /// Create new customer
        /// </summary>
        /// <returns>Customer</returns>
        public ICustomer CreateCustomer()
        {
            return new CustomerDto() { Orders = new List<OrderDto>() };
        }

        /// <summary>
        /// Create new order for customer
        /// </summary>
        /// <returns>Order</returns>
        public IOrder CreateOrder(ICustomer customer)
        {
            return new OrderDto() { CustomerId = customer.Id };
        }

        public IOrder SaveOrder(IOrder order)
        {
            if (order == null)
                return null;

            var customer = FakeCustomerDb.FirstOrDefault(c => c.Id == order.CustomerId);

            if (customer == null)
                return null;

            var orderDto = ConvertToOrderDto(order);
            customer.Orders.Add(orderDto);

            orderDto.Id = ++idOrder;
            return orderDto;
        }

        /// <summary>
        /// Add new customer to DB
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Customer</returns>
        public ICustomer SaveCustomer(ICustomer customer)
        {
            var result = ConvertToCustomerDto(customer, true);
            result.Id = ++idCustomer;
            FakeCustomerDb.Add(result);
            return result;
        }

        /// <summary>
        /// Convert customer model abstraction to DTO
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="includeOrders">Include orders inside</param>
        /// <returns>Customer abstraction</returns>
        private CustomerDto ConvertToCustomerDto(ICustomer customer, bool includeOrders)
        {
            var resultCustomer = new CustomerDto() { Id = customer.Id, Email = customer.Email, Name = customer.Name };
            if (includeOrders && customer.Orders != null)
                resultCustomer.Orders = customer.Orders.Select(ConvertToOrderDto).ToList();
            return resultCustomer;
        }

        /// <summary>
        /// Convert order model abstraction to DTO
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Order abstraction</returns>
        private OrderDto ConvertToOrderDto(IOrder order)
        {
            return new OrderDto() { Id = order.Id, CreatedDate = order.CreatedDate, CustomerId = order.CustomerId, Price = order.Price };
        }

        /// <summary>
        /// Create customer report
        /// </summary>
        /// <returns>Report</returns>
        public ICustomerReport CreateCustomerReport()
        {
            var orders = FakeCustomerDb.SelectMany(c => c.Orders).Where(o => o != null);
            var report = new dto.report.CustomerReportDto() {
                TotalCustomerCount = FakeCustomerDb.Count(),
                TotalOrderCount = orders.Count(),
                AvgPrice = orders.Count() == 0 ? 0 : orders.Select(o => o.Price).Average()
            };
            return report;
        }

        /// <summary>
        /// Create new business logic instance
        /// </summary>
        /// <returns>Business logic</returns>
        public static IBusinessLogic Create()
        {
            return new Engine();
        }

        public void Dispose()
        {
            //
        }
    }
}
