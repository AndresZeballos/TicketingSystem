using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Controllers.Filters;
using TicketingSystem.Coordinators;

namespace TicketingSystem.Controllers
{
    [Authorize, AdminAccess]
    public class AdminController : Controller
    {
        IUserCoordinator coordinator;

        public AdminController(IUserCoordinator userCoordinator)
        {
            coordinator = userCoordinator;
        }

        public ActionResult Index()
        {
            return View(coordinator.GetUsers());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnableUser(int id)
        {
            coordinator.Enable(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisableUser(int id)
        {
            coordinator.Disable(id);
            return RedirectToAction("Index");
        }
    }
}