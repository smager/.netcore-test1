using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using netzwelt_gmfuentes.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace netzwelt_gmfuentes.Controllers
{

    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Validate(User user)
        {
            var _loginInfo = new User() { username = user.username, password= user.password };
            using (var client = new HttpClient())
            {
                var  _res = await client.PostAsync(
                      "https://netzwelt-devtest.azurewebsites.net/Account/SignIn"
                    , new StringContent(JsonConvert.SerializeObject(_loginInfo)
                    , Encoding.UTF8
                    , "application/json"
                ));

                if (_res.StatusCode == HttpStatusCode.OK)
                {
                    await this.SignInUser(user.username, false);
                    return RedirectToAction("index", "home");
                }

            }

            return RedirectToAction("login", "account");

        }

        private async Task SignInUser(string username, bool isPersistent)
        {
            var claims = new List<Claim>();

            try
            {
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimIdenties);
                var authenticationManager = Request.HttpContext;

                await authenticationManager.SignInAsync(
                      CookieAuthenticationDefaults.AuthenticationScheme
                    , claimPrincipal
                    , new AuthenticationProperties() {
                        IsPersistent = isPersistent
                        ,ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                    });


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
