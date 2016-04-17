using System.Web.Http;
using TestApplication.Models;
using TestApplication.BusinessLogic;

namespace TestApplication.Controllers
{
    public class ReportsController : ApiController
    {
        [Route("api/ReportsController/Total")]
        public CustomerReport GetCustomerTotalReport()
        {
            using (var logic = Engine.Create())
            {
                return CustomerReport.GetByBusinessLogic(logic.CreateCustomerReport());
            }
        }
    }
}
