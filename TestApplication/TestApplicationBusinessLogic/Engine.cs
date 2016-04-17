using System;
using System.Collections.Generic;
using System.Linq;
using TestApplication.Common;
using TestApplication.Common.reports;
using TestApplication.BusinessLogic.dto;
using AutoMapper;

namespace TestApplication.BusinessLogic
{
    public class Engine : IBusinessLogic
    {
        private Repository.RepositoryContext db;

        static Engine()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Repository.dbo.OrderDbo, OrderDto>();
                cfg.CreateMap<OrderDto, Repository.dbo.OrderDbo>();
                cfg.CreateMap<IOrder, Repository.dbo.OrderDbo>();
                cfg.CreateMap<Repository.dbo.CustomerDbo, CustomerDto>()
                    .ForMember(dest => dest.Orders, opt => opt.Ignore());
                cfg.CreateMap<CustomerDto, Repository.dbo.CustomerDbo>();
                cfg.CreateMap<ICustomer, Repository.dbo.CustomerDbo>();
            });
        }

        /// <summary>
        /// User can't create business logic object
        /// </summary>
        private Engine()
        {
            db = new Repository.RepositoryContext();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <param name="includeOrders">True to include orders</param>
        /// <returns>Customer array</returns>
        public ICustomer[] GetCustomers()
        {
            return db.Customers.ToArray().Select(i => Mapper.Map<Repository.dbo.CustomerDbo, CustomerDto>(i, opt => opt.AfterMap((src, dst) =>
            {
                dst.Orders = new List<OrderDto>();
            }))).ToArray();
        }

        /// <summary>
        /// Get customer by identifier
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer. NUll if not found</returns>
        public ICustomer GetCustomer(long customerId)
        {
            var foundedCustomer = db.Customers.FirstOrDefault(c => c.Id == customerId);
            return (foundedCustomer == null)
                ? null
                : Mapper.Map<Repository.dbo.CustomerDbo, CustomerDto>(foundedCustomer, opt => opt.AfterMap((src,dst) => 
                {
                    dst.Orders = src.Orders.Select(o => Mapper.Map<OrderDto>(o)).ToList();
                }));
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

            var customer = db.Customers.FirstOrDefault(c => c.Id == order.CustomerId);

            if (customer == null)
                return null;

            var dbOrder = db.Orders.FirstOrDefault(o => o.Id == order.Id) ?? new Repository.dbo.OrderDbo();
            Mapper.Map(order, dbOrder, opt => opt.AfterMap((src,dst) => {
                dst.Customer = customer;
            }));
            if (dbOrder.Id == 0)
                db.Orders.Add(dbOrder);

            db.SaveChanges();
            return Mapper.Map<OrderDto>(dbOrder);
        }

        /// <summary>
        /// Add new customer to DB
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Customer</returns>
        public ICustomer SaveCustomer(ICustomer customer)
        {
            if (customer == null)
                return null;

            var dbCustomer = db.Customers.FirstOrDefault(c => c.Id == customer.Id) ?? new Repository.dbo.CustomerDbo();
            Mapper.Map(customer, dbCustomer);
            if (dbCustomer.Id == 0)
                db.Customers.Add(dbCustomer);
            db.SaveChanges();
            return Mapper.Map<Repository.dbo.CustomerDbo, CustomerDto>(dbCustomer, opt => opt.AfterMap((src, dst) =>
            {
                dst.Orders = new List<OrderDto>();
            }));
        }

        /// <summary>
        /// Create customer report
        /// </summary>
        /// <returns>Report</returns>
        public ICustomerReport CreateCustomerReport()
        {
            var ordersCount = db.Orders.Count();
            var report = new dto.report.CustomerReportDto() {
                TotalCustomerCount = db.Customers.Count(),
                TotalOrderCount = ordersCount,
                AvgPrice = ordersCount == 0 ? 0 : db.Orders.Select(o => o.Price).Average()
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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                    db = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
