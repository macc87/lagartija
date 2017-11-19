using System.Globalization;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Mvc.Html;
using Fantasy.API.Host.Models;
using Fantasy.API.Utilities.HttpClient;
using Fantasy.API.Utilities.ServicesHandler;

namespace Fantasy.API.Host.Controllers
{
    [Authorize]
    [System.Web.Mvc.RoutePrefix("account")]
    public class AccountController : Controller
    {
        private AuthenticationContext context;
        //private ILdapUtility ldap;

        /// <summary>
        /// Public Constructor
        /// </summary>
        public AccountController()
        {
            context = new AuthenticationContext();
            //ldap = new LdapUtilitity(context);
        }

        // GET: /Account/Login
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("Login", Name = "LoginRoute")]
        public ActionResult Login(String returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.Route("Login", Name = "PostLoginRoute")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            Session.RemoveAll();
            if (ModelState.IsValid)
            {
                try
                {
                    var roles = new List<string>();
                    var isIt = false;
                    //if (await ldap.AuthenticateCredentialsWithGroupInLdap(model.UserName, model.Password, ApplicationRoles.ItAdmin))
                    ///TODO Add authentication using database
                    if (model.UserName == "admin" && model.Password == "password")
                    {
                        isIt = true;
                        roles.Add(ApplicationRoles.ItAdmin);
                    }

                    if (roles.Any())
                    {
                        //var user = await ldap.FindByUserId(model.UserName, model.UserType);
                        var user = new
                        {
                            FirstName = "Ivan",
                            LastName = "Hernandez",
                            EmailAddress = "ihferrero82@gmail.com"
                        };

                        var claims = new List<Claim>{
                            new Claim(ClaimTypes.NameIdentifier, model.UserName),
                            new Claim(ClaimTypes.Name, user.FirstName +" "+user. LastName),
                            new Claim(ClaimTypes.IsPersistent, model.RememberMe.ToString()),
                            new Claim(ClaimTypes.Expiration,DateTime.Now.AddHours(24).ToString(CultureInfo.InvariantCulture)),
                            new Claim(ClaimTypes.Email, user.EmailAddress),
                            new Claim("ApplicationName", Applications.Fantasy),
                            new Claim("CompanyName", Applications.Fantasy),
                            new Claim("CompanyId", Applications.Fantasy)
                        };

                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                        var owinContext = Request.GetOwinContext();
                        var authenticationManager = owinContext.Authentication;

                        authenticationManager.SignIn(claimsIdentity);
                        Response.Cookies.Add(new HttpCookie("_error", "Authorized"));
                        Session.Add("userToken", user);

                   
                        return RedirectToLocal();
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        return View(model);
                    }
                }
                catch (HttpApiRequestException ex)
                {
                    if (HttpStatusCode.BadRequest == ex.StatusCode)
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            }
            return await Task.FromResult(View(model));
        }

        // POST: /Account/LogOff
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.Route("logoff", Name = "LogOffRoute")]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
