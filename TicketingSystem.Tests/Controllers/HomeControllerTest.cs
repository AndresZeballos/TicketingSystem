using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TicketingSystem;
using TicketingSystem.Controllers;
using TicketingSystem.Coordinators;

namespace TicketingSystem.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            HomeController controller = GetController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Register()
        {
            HomeController controller = GetController();
            ViewResult result = controller.Register() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void UserNotEnabled()
        {
            HomeController controller = GetController();
            ViewResult result = controller.UserNotEnabled() as ViewResult;
            Assert.IsNotNull(result);
        }

        private HomeController GetController()
        {
            return new HomeController(new UserCoordinator(new Models.Model()));
        }
    }
}
