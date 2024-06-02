using Microsoft.AspNetCore.Mvc;

namespace ChildrenTransport.Web.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult ProvideBankingDetails()
        {
            return View();
        }

        public IActionResult MakePayment()
        {
            return View();
        }
    }
}
