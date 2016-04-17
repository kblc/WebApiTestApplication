using System.Web.Http;
using TestApplication.Models;
using TestApplication.BusinessLogic;

namespace TestApplication.Controllers
{
    [RoutePrefix("api/reports")]
    public class ReportsController : ApiController
    {
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
