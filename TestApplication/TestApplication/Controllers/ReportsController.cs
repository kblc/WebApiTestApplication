using System.Web.Http;
using TestApplication.Models;
using TestApplication.BusinessLogic;

namespace TestApplication.Controllers
{
    [RoutePrefix("api/reports")]
    public class ReportsController : ApiController
    {
        /// <summary>
        /// Get customer's total report
        /// </summary>
        /// <remarks>GET api/reports/total</remarks>
        /// <returns>Report</returns>
        [HttpGet]
        [Route("total")]
        public CustomerReport GetCustomerTotalReport()
        {
            using (var logic = Engine.Create())
            {
                return CustomerReport.GetByBusinessLogic(logic.CreateCustomerReport());
            }
        }
    }
}
