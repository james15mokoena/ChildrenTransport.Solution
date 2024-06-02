using Microsoft.AspNetCore.Mvc;
using ChildrenTransport.Web.Models;
using ChildrenTransport.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ChildrenTransport.Web.Controllers
{
    public class AdministrationController(AppDbContext dbContext) : Controller
    {
        private readonly AppDbContext database = dbContext;
       
        public IActionResult RegisterTransport(Taxi? taxi)
        {
            if (HttpContext.Session.GetString("adminEmail") != null && HttpContext.Session.GetString("adminEmail") != "")
            {
                ViewData["Logged_In"] = "Admin";

                if (taxi != null && taxi.NumSeats != 0 && taxi.Image != null && taxi.Name != null)
                {
                    var exTaxi = database.Taxis.FirstOrDefault(t => t.Name == taxi.Name || t.Image == taxi.Image);

                    if (exTaxi == null)
                    {
                        database.Taxis.Add(taxi);

                        try
                        {
                            int response = database.SaveChanges();
                            if (response > 0)
                                TempData["Response"] = "Successfully registered the transport.";
                            
                        }
                        catch (Exception ex)
                        {
                            TempData["Error"] = ex.InnerException;
                        }                        
                    }
                    else
                    {
                        TempData["Error"] = "Transport already exists!";
                    }
                }

                return View();
            }

            return RedirectToAction("AdminLogin", "Authentication");
        }

        public IActionResult UpdateTransport(Taxi? taxi, string? option)
        {

            if (HttpContext.Session.GetString("adminEmail") != null && HttpContext.Session.GetString("adminEmail") != "")
            {
                ViewData["Logged_In"] = "Admin";

                Taxi? existingTaxi = null;
                bool doesExist = false;

                if (taxi != null)
                    existingTaxi = database.Taxis.FirstOrDefault(t => t.Id == taxi.Id);

                if (taxi != null && existingTaxi != null && option == "update")
                {
                    int countChanges = 0;

                    if (taxi.Name != null && taxi.Name != "")
                    {
                        existingTaxi.Name = taxi.Name;
                        ++countChanges;
                    }

                    if (taxi.NumSeats > 0)
                    {
                        existingTaxi.NumSeats = taxi.NumSeats;
                        ++countChanges;
                    }

                    if (taxi.Image != null && taxi.Image != "")
                    {
                        existingTaxi.Image = taxi.Image;
                        ++countChanges;
                    }
                    if (taxi.Quantity > 0)
                    {
                        existingTaxi.Quantity = taxi.Quantity;
                        ++countChanges;
                    }

                    database.Taxis.Update(existingTaxi);
                    doesExist = true;

                    if (countChanges > 0)
                        TempData["Response"] = "Successfully updated.";
                    else
                        TempData["Response"] = "No changes were made.";
                }
                else if (existingTaxi != null && option == "delete")
                {
                    database.Taxis.Remove(existingTaxi);
                    doesExist = true;
                    TempData["Response"] = "Successfully deleted.";
                }
                else if (existingTaxi != null && option == "getdata")
                {
                    return RedirectToAction("UpdateTransport", "Administration", existingTaxi);
                }

                try
                {
                    if (doesExist)
                    {
                        database.SaveChanges();
                        return View();
                    }

                    if (option != null && (option == "update" || option == "delete" || option == "getdata"))
                    {
                        TempData["Error"] = "This transport does not exist!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }

                return View();
            }

            return RedirectToAction("AdminLogin", "Authentication");
        }

        public IActionResult AdminOptions()
        {
            if (HttpContext.Session.GetString("adminEmail") != null && HttpContext.Session.GetString("adminEmail") != "")
            {
                ViewData["Logged_In"] = "Admin";
                return View();
            }
                
            return RedirectToAction("AdminLogin", "Authentication");
        }

        public IActionResult DisplayAdmins()
        {
            if(HttpContext.Session.GetString("adminEmail") != null && HttpContext.Session.GetString("adminEmail") != "")
            {
                ViewData["Logged_In"] = "Admin";

                var admins = database.Administrators.DefaultIfEmpty();

                if(admins != null)
                {

                    return View(admins.ToList());
                }
                
            }

            return RedirectToAction("AdminLogin", "Authentication");
        }

        public IActionResult Invoices()
        {
            return View();
        }

        public IActionResult Report()
        {
            return View();
        }

        public IActionResult Children()
        {
            return View();
        }

        public IActionResult Parents()
        {
            return View();
        }

        public IActionResult UpdateParent()
        {
            return View();
        }

        public IActionResult UpdateChild()
        {
            return View();
        }

        public IActionResult DisplayTransports()
        {
            if (HttpContext.Session.GetString("adminEmail") != null && HttpContext.Session.GetString("adminEmail") != "")
            {
                ViewData["Logged_In"] = "Admin";

                var transports = database.Taxis.DefaultIfEmpty();

                if (transports != null && transports.Any())
                {
                    return View(transports.ToList());
                }

                TempData["Response"] = "No transports available yet.";

                return View();
            }

            return RedirectToAction("AdminLogin","Authentication");
        }

        [HttpGet]
        public IActionResult GetTransportData(Taxi taxi)
        {
            if (HttpContext.Session.GetString("adminEmail") != null && HttpContext.Session.GetString("adminEmail") != "")
            {
                ViewData["Logged_In"] = "Admin";

                var transport = database.Taxis.FirstOrDefault(t => t.Id == taxi.Id);

                if (transport != null)
                {
                    return RedirectToAction("UpdateTransport", "Administration", transport);
                }

                TempData["Error"] = "The identifier doesn't match any transport.";

                return View();
            }

            return RedirectToAction("AdminLogin", "Authentication");
        }

        public async Task<IActionResult> AdminUpdate(Administrator? admin, string? option)
        {

            if (HttpContext.Session.GetString("adminEmail") != null && HttpContext.Session.GetString("adminEmail") != "")
            {
                ViewData["Logged_In"] = "Admin";

                bool updatedAtLeastOne = false;
                string? s = option;

                if (admin != null)
                {
                    var administrator = await database.Administrators.Where(a => a.Email.Equals(admin.Email) && a.Password.Equals(admin.Password)).FirstOrDefaultAsync();

                    // update code
                    if (administrator != null && option != null && option == "update")
                    {
                        if (admin.FirstName != null && admin.FirstName != "")
                        {
                            administrator.FirstName = admin.FirstName;
                            updatedAtLeastOne = true;
                        }

                        if (admin.LastName != null && admin.LastName != "")
                        {
                            administrator.LastName = admin.LastName;
                            updatedAtLeastOne = true;
                        }

                        if (admin.Phone != null && admin.Phone != "")
                        {
                            administrator.Phone = admin.Phone;
                            updatedAtLeastOne = true;
                        }

                        if (updatedAtLeastOne)
                        {
                            try
                            {
                                database.Administrators.Update(administrator);
                                await database.SaveChangesAsync();
                                TempData["Response"] = "Successfully updated the administrator.";
                            }
                            catch (Exception e)
                            {
                                TempData["Response"] = e.Message;
                            }
                        }
                    }
                    // delete code
                    else if (administrator != null && option != null && option == "delete")
                    {
                        try
                        {
                            database.Administrators.Remove(administrator);
                            await database.SaveChangesAsync();
                            TempData["Response"] = "Successfully delete the administrator.";
                        }
                        catch (Exception e)
                        {
                            TempData["Response"] = e.Message;
                        }
                    }
                }

                return View();
            }
            else
                return RedirectToAction("AdminLogin", "Authentication");            
        }
       
    }
}
