using ChildrenTransport.Web.Models;
using Microsoft.AspNetCore.Mvc;
using ChildrenTransport.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ChildrenTransport.Web.Controllers
{
    public class AuthenticationController(AppDbContext dbase) : Controller
    {

        private readonly AppDbContext database = dbase;

        public IActionResult ParentLogin()
        {
            return View();
        }        

        public IActionResult AdminLogin()
        {            
            return View();
        }

        public IActionResult AdminLogout()
        {
            if(CheckLoggedIn("adminEmail"))
            {
                HttpContext.Session.Remove("adminEmail");
                ViewData["Logged_In"] = null;                
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult ParentLogout()
        {
            if (CheckLoggedIn("ParentID"))
            {
                HttpContext.Session.Remove("ParentID");
                ViewData["Logged_In"] = null;
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Use hashing to securely store password.
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public IActionResult ProcessAdminLogin(Administrator admin)
        {
            var admini = database.Administrators.FirstOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);
            
            // admin exists.
            if (admini != null)
            {
                HttpContext.Session.SetString("adminEmail", admini.Email);
                ViewData["Logged_In"] = "Admin";

                return RedirectToAction("AdminOptions","Administration");
            }

            TempData["Error"] = "Admininstrator does not exist!";

            return RedirectToAction("AdminLogin","Authentication");
        }

        public async Task<IActionResult> ProcessParentLogin(Parent par)
        {
            var parent = await database.Parents.FirstOrDefaultAsync(p => p.ParentId == par.ParentId && p.Password == par.Password);

            if (parent != null)
            {

                HttpContext.Session.SetString("ParentID", parent.ParentId);
                ViewData["Logged_In"] = "Parent";

                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Parent does not exist.";

            return RedirectToAction("ParentLogin", "Authentication");
        }


        public IActionResult AdminCreation()
        {
            if (CheckLoggedIn("adminEmail"))
            {
                ViewData["Logged_In"] = "Admin";

                return View();
            }

            return RedirectToAction("AdminLogin", "Authentication");
        }

        /// <summary>
        /// Admin password must be hashed securely before being stored.
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public IActionResult ProcessAdminCreation(Administrator admin)
        {
            if (CheckLoggedIn("adminEmail"))
            {
                ViewData["Logged_In"] = "Admin";

                var admini = database.Administrators.FirstOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);

                if (admini == null)
                {

                    database.Administrators.Add(admin);

                    try
                    {
                        database.SaveChanges();
                        TempData["Note"] = "Successfully created administrator account.";
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ex.InnerException;
                        return RedirectToAction("AdminCreation", "Authentication");
                    }

                }

                TempData["Error"] = "Administrator already exists.";

                return RedirectToAction("AdminCreation", "Authentication");
            }

            return RedirectToAction("AdminLogin", "Authentication");
        }

        public async Task<IActionResult> ProcessParentCreation(Parent parent)
        {
            var ex = await database.Parents.FirstOrDefaultAsync(p => p.ParentId == parent.ParentId && p.Password == parent.Password);

            if(ex == null)
            {
                try
                {
                    await database.Parents.AddAsync(parent);
                    await database.SaveChangesAsync();
                    TempData["Success"] = "Successfully created a parent account.";

                    return RedirectToAction("ParentRegistration", "Application");
                }
                catch(Exception e)
                {
                    TempData["Error"] = e.InnerException;
                }
            }
                       
            TempData["Error"] = "Parent already exists.";
            return View();
        }

        private bool CheckLoggedIn(string who)
        {
            return HttpContext.Session.GetString(who) != null && HttpContext.Session.GetString(who) != "";
        }
    }
}
