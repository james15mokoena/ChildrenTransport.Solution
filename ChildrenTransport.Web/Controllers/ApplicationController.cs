using ChildrenTransport.Web.Data;
using ChildrenTransport.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChildrenTransport.Web.Controllers
{
    public class ApplicationController(AppDbContext dbcontext) : Controller
    {
        private readonly AppDbContext database = dbcontext;

        public IActionResult ParentRegistration()
        {
            return View();
        }

        public IActionResult ChildRegistration()
        {            
            if(HttpContext.Session.GetString("ParentID") != null && HttpContext.Session.GetString("ParentID") != "")
            {
                ViewData["Logged_In"] = "Parent";
            }

            return View();
        }

        public async Task<IActionResult> ProcessChildApplication(Child child)
        {
            var parent = await database.Parents.FirstOrDefaultAsync(p => p.ParentId == child.ParentId);
            var chld = await database.Children.FirstOrDefaultAsync(c => c.ChildId == child.ChildId);

            if (HttpContext.Session.GetString("ParentID") != null && HttpContext.Session.GetString("ParentID") != "")
            {
                ViewData["Logged_In"] = "Parent";

                if (parent != null && chld == null)
                {

                    try
                    {
                        //child.Parent = parent;
                        await database.Children.AddAsync(child);
                        await database.SaveChangesAsync();
                        TempData["Success"] = "Successfully registered the child.";
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ex.InnerException;
                    }
                }
                else
                {
                    TempData["Error"] = "Parent ID does not exist in the database OR The child is already registered.";
                }
            }
            else
            {
                TempData["Error"] = "Please login before applying for the child.";
                return RedirectToAction("ParentLogin", "Authentication");
            }            

            return RedirectToAction("ChildRegistration", "Application");
        }

        public async Task<IActionResult> AddressRegistration(Address? address)
        {
            if (HttpContext.Session.GetString("ParentID") != null && HttpContext.Session.GetString("ParentID") != "")
            {
                ViewData["Logged_In"] = "Parent";

                if (address != null && address.ChildId != null && address.ParentId != null) {

                    var addr = await database.Addresses.FirstOrDefaultAsync(a => a.ChildId == address.ChildId);

                    if (addr == null)
                    {
                        try
                        {
                            await database.Addresses.AddAsync(address);
                            await database.SaveChangesAsync();
                            TempData["Success"] = "Successfully registered the address.";
                        }
                        catch (Exception ex)
                        {
                            TempData["Error"] = ex.InnerException;
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Address already registered for the child.";
                    }
                }
            }
            else
            {
                TempData["Error"] = "Please login before registering the child's address.";
                return RedirectToAction("ParentLogin", "Authentication");
            }

            return View();
        }

    }
}
