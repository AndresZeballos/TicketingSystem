using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers.Filters
{
    public class AdminAccess : ActionFilterAttribute
    {
        private Model db = new Model();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string userName = null;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                userName = HttpContext.Current.User.Identity.Name;
                User u = db.Users.Where(r => r.Email == userName).AsNoTracking().First();
                if (!u.IsAdmin)
                {
                    filterContext.Result = new RedirectResult("~/DashBoard");
                }
            }
        }
    }
}