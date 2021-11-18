using Microsoft.AspNetCore.Mvc;
using netzwelt_gmfuentes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace netzwelt_gmfuentes.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            if ( ! this.User.Identity.IsAuthenticated)
            {
                // Home Page.  
                return RedirectToAction("login", "account");
            }
            return View();
        }

       
 
    }
}
