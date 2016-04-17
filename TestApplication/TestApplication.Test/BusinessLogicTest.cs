using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Test
{
    [TestClass]
    public class BusinessLogicTest
    {
        [TestMethod]
        public void Customers()
        {
            using (var logic = BusinessLogic.Engine.Create())
            {
                var firstReport = logic.CreateCustomerReport();

                var newCustomer = logic.CreateCustomer();
                Assert.AreEqual(0, newCustomer.Id, "Customer identifier must be empty");
                newCustomer.Email = "test@email";
                newCustomer.Name = "test";
                var savedCustomer = logic.SaveCustomer(newCustomer);
                Assert.AreNotEqual(0, savedCustomer.Id, "Customer identifier must be filed");
                Assert.AreEqual(0, savedCustomer.Orders.Count(), "Customer order's count must be empty");

                var alreadySavedCustomer = logic.GetCustomer(savedCustomer.Id);
                Assert.AreEqual(savedCustomer.Email, alreadySavedCustomer.Email);
                Assert.AreEqual(savedCustomer.Name, alreadySavedCustomer.Name); 
                Assert.AreEqual(savedCustomer.Id, alreadySavedCustomer.Id);

                var secondReport = logic.CreateCustomerReport();
                Assert.AreEqual(firstReport.TotalCustomerCount + 1, secondReport.TotalCustomerCount);
            }
        }

        [TestMethod]
        public void CustomersAndOrders()
        {
            using (var logic = BusinessLogic.Engine.Create())
            {
                var firstReport = logic.CreateCustomerReport();

                var newCustomer = logic.CreateCustomer();
                Assert.AreEqual(0, newCustomer.Id, "Customer identifier must be empty");
                newCustomer.Email = "test@email";
                newCustomer.Name = "test";
                newCustomer = logic.SaveCustomer(newCustomer);

                var newOrder = logic.CreateOrder(newCustomer);
                newOrder.Price = 123.456m;
                newOrder = logic.SaveOrder(newOrder);

                Assert.AreNotEqual(0, newOrder.Id, "Order identifier must be filed");
                Assert.AreEqual(123.456m, newOrder.Price, "Order price value must be 123.456");

                newCustomer = logic.GetCustomer(newCustomer.Id);
                Assert.AreEqual(1, newCustomer.Orders.Count(), "Customer order's count mustn't be empty");

                var secondReport = logic.CreateCustomerReport();
                Assert.AreEqual(firstReport.TotalCustomerCount + 1, secondReport.TotalCustomerCount);
                Assert.AreEqual(firstReport.TotalOrderCount + 1, secondReport.TotalOrderCount);
            }
        }

        [TestMethod]
        public void ReportCalculation()
        {
            var repDto = new BusinessLogic.dto.report.CustomerReportDto()
            {
                TotalCustomerCount = 10,
                TotalOrderCount = 10,
            };
            Assert.AreEqual(1, repDto.AvgOrdersPerCustomer, "Average value must equals 1");

            var repDto2 = new BusinessLogic.dto.report.CustomerReportDto()
            {
                TotalCustomerCount = 0,
                TotalOrderCount = 0,
            };
            Assert.AreEqual(0, repDto2.AvgOrdersPerCustomer, "Average value must equals 0");
        }
    }
}
