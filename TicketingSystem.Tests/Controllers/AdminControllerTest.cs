using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingSystem.Controllers;
using TicketingSystem.Coordinators;
using TicketingSystem.Models;

namespace TicketingSystem.Tests.Controllers
{
    [TestFixture]
    public class AdminControllerTest
    {
        [Test]
        public void Index()
        {
            AdminController controller = GetController();
            _mockCoordinator.Setup(m => m.GetUsers()).Returns(Enumerable.Empty<User>().ToList());
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void DisableUser()
        {
            AdminController controller = GetController();
            _mockCoordinator.Setup(m => m.Disable(It.IsAny<int>()));
            RedirectToRouteResult result = controller.DisableUser(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void EnableUser()
        {
            AdminController controller = GetController();
            _mockCoordinator.Setup(m => m.Enable(It.IsAny<int>()));
            RedirectToRouteResult result = controller.EnableUser(1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
        }
        
        [SetUp]
        public void Setup()
        {
            _mockCoordinator = new Mock<IUserCoordinator>();
        }

        private AdminController GetController()
        {
            return new AdminController(_mockCoordinator.Object);
        }

        private Mock<IUserCoordinator> _mockCoordinator;
    }
}
