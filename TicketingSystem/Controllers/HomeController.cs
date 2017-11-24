using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TicketingSystem.Coordinators;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class HomeController : Controller
    {
        private IUserCoordinator coordinator;

        public HomeController(IUserCoordinator userCoordinator)
        {
            coordinator = userCoordinator;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Password")] User user)
        {
            if (!coordinator.IsEnabled(user))
            {
                return RedirectToAction("UserNotEnabled", "Home");
            }
            if (coordinator.ValidateLogin(user))
            {
                FormsAuthentication.SetAuthCookie(user.Name, false);

                var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(1), false, "");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                
                Session["userId"] = coordinator.GetUser(user)?.Id;

                User dbUser = coordinator.GetUser(user);
                if (dbUser.IsAdmin)
                    return RedirectToAction("Index", "Admin");
                return RedirectToAction("Index", "Dashboard");
            }
            return RedirectToAction("Index", "Home", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["userId"] = null;
            return RedirectToAction("Index", "Home");
        }
        
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Email,Password,Name")] User user)
        {
            if (ModelState.IsValid)
            {
                coordinator.Create(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }


        [Route("UserNotEnabled")]
        public ActionResult UserNotEnabled()
        {
            return View();
        }
        
    }
}