using ChildrenTransport.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ChildrenTransport.Web.Data;

namespace ChildrenTransport.Web.Controllers
{
    public class HomeController(AppDbContext dbContext) : Controller
    {
        private readonly AppDbContext dbContext = dbContext;

        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("adminEmail") != null && HttpContext.Session.GetString("adminEmail") != "")
                ViewData["Logged_In"] = "Admin";
            else if (HttpContext.Session.GetString("ParentID") != null && HttpContext.Session.GetString("ParentID") != "")
                ViewData["Logged_In"] = "Parent";

            var taxis = dbContext.Taxis.DefaultIfEmpty();

            if(taxis != null)
            {
                return View(taxis.ToList());
            }
            return View();
        }

        public IActionResult Invoice()
        {
            if(HttpContext.Session.GetString("ParentID") != null && HttpContext.Session.GetString("ParentID") != "")
            {
                ViewData["Logged_In"] = "Parent";

                // Later

            }

            return RedirectToAction("ParentLogin", "Authentication");
        }

        public IActionResult AddReview()
        {
            return View();
        }

        public IActionResult DisplayReviews()
        {
            return View();
        }

        public IActionResult TransportDetails()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
