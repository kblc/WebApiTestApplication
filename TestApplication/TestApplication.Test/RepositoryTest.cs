using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TestApplication.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void Customers()
        {
            using(var db = new Repository.RepositoryContext())
            {
                var customersCount = db.Customers.Count();
                var customersCountAfterInsert = 0;
                var customersCountAfterDelete = 0;
                var newCustomer = new Repository.dbo.CustomerDbo { Email = "test@email", Name = "test" };
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                Assert.AreNotEqual(0, newCustomer.Id, "Customer must have an identifier");
                using(var db2 = new Repository.RepositoryContext())
                {
                    customersCountAfterInsert = db2.Customers.Count();
                }
                db.Customers.Remove(newCustomer);
                db.SaveChanges();
                Assert.AreEqual(customersCount + 1, customersCountAfterInsert, "Customer count must increase by one after insertion");
                using (var db2 = new Repository.RepositoryContext())
                {
                    customersCountAfterDelete = db2.Customers.Count();
                }
                Assert.AreEqual(customersCount, customersCountAfterDelete, "Customer count must decrease by one after deletion");
            }
        }

        [TestMethod]
        public void Orders()
        {
            using (var db = new Repository.RepositoryContext())
            {
                var newCustomer = new Repository.dbo.CustomerDbo { Email = "test@email", Name = "test" };
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                try
                {
                    using (var db3 = new Repository.RepositoryContext())
                    {
                        db3.Customers.Attach(newCustomer);
                        var ordersCount = db3.Orders.Count();
                        var ordersCountAfterInsert = 0;
                        var ordersCountAfterDelete = 0;
                        var newOrder = new Repository.dbo.OrderDbo { Price = 123.456m, CreatedDate = DateTime.UtcNow, Customer = newCustomer };
                        db3.Orders.Add(newOrder);
                        db3.SaveChanges();
                        Assert.AreNotEqual(0, newOrder.Id, "Order must have an identifier");
                        using (var db2 = new Repository.RepositoryContext())
                        {
                            ordersCountAfterInsert = db2.Orders.Count();
                        }
                        db3.Orders.Remove(newOrder);
                        db3.SaveChanges();
                        Assert.AreEqual(ordersCount + 1, ordersCountAfterInsert, "Order count must increase by one after insertion");
                        using (var db2 = new Repository.RepositoryContext())
                        {
                            ordersCountAfterDelete = db2.Orders.Count();
                        }
                        Assert.AreEqual(ordersCount, ordersCountAfterDelete, "Order count must decrease by one after deletion");
                    }
                }
                finally
                {
                    db.Customers.Remove(newCustomer);
                    db.SaveChanges();
                }
            }
        }
    }
}
