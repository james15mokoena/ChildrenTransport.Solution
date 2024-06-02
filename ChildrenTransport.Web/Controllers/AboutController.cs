using Microsoft.AspNetCore.Mvc;

namespace ChildrenTransport.Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
