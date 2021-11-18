using Microsoft.AspNetCore.Mvc;
using netzwelt_gmfuentes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace netzwelt_gmfuentes.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                // Home Page.  
                return RedirectToAction("login", "account");
            }
            var territories = new Territories();
            var client = new HttpClient();

            var _res = await client.GetAsync("https://netzwelt-devtest.azurewebsites.net/Territories/All");

            if (_res.StatusCode == HttpStatusCode.OK)
            {
                var content = _res.Content;
                var data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    territories = JsonConvert.DeserializeObject<Territories>(data);

                    List<Territory> _te = territories.Data
                    .Where(e => e.Parent == null) 
                    .Select(e => new Territory
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Parent = e.Parent,
                        Children = GetChildren(territories.Data, e.Id) 
                    }).ToList();

                    ViewBag.Territories = _te;

                }
                
            }

            return View();
        }

        private static List<Territory> GetChildren(List<Territory> items, int parentId)
        {
            return items
                .Where(x => x.Parent == parentId)
                .Select(e => new Territory
                {
                    Id = e.Id,
                    Name = e.Name,
                    Parent = e.Parent,
                    Children = GetChildren(items, e.Id)
                  
                }).ToList();
        }


    }
}
