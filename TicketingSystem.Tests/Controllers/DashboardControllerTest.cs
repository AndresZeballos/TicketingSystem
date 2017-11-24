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
    public class DashboardControllerTest
    {

        [Test]
        public void Index()
        {
            var user = GetUser();
            var tickets = GetTicketsList();
            DashboardController controller = GetController();
            _mockUserCoordinator.Setup(m => m.GetUserByEmail(It.IsAny<string>())).Returns(user);
            _mockTicketCoordinator.Setup(m => m.GetTicketsByUser(It.IsAny<User>())).Returns(tickets);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateTicket()
        {
            var user = GetUser();
            var tickets = GetTicketsList();
            DashboardController controller = GetController();
            _mockUserCoordinator.Setup(m => m.GetUserByEmail(It.IsAny<string>())).Returns(user);
            _mockUserCoordinator.Setup(m => m.GetUsers()).Returns(GetUsersList());
            
            ViewResult result = controller.CreateTicket() as ViewResult;

            Assert.IsNotNull(result);
        }
        
        [Test]
        public void EditTicket()
        {
            var user = GetUser();
            var ticket = GetTicket();
            DashboardController controller = GetController();
            _mockTicketCoordinator.Setup(m => m.GetTicketsById(It.IsAny<int>())).Returns(ticket);
            _mockUserCoordinator.Setup(m => m.GetUsers()).Returns(GetUsersList());

            ViewResult result = controller.EditTicket(ticket.Id) as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteTicket()
        {
            var user = GetUser();
            var ticket = GetTicket();
            DashboardController controller = GetController();
            _mockTicketCoordinator.Setup(m => m.GetTicketsById(It.IsAny<int>())).Returns(ticket);
            _mockUserCoordinator.Setup(m => m.GetUsers()).Returns(GetUsersList());

            ViewResult result = controller.DeleteTicket(ticket.Id) as ViewResult;

            Assert.IsNotNull(result);
        }

        [SetUp]
        public void Setup()
        {
            _mockTicketCoordinator = new Mock<ITicketCoordinator>();
            _mockUserCoordinator = new Mock<IUserCoordinator>();
        }

        private User GetUser()
        {
            return new User() { Id = 1, Email = "example@domain.com", Password = "theUserPass", Name = "John Doe", IsEnable = true };
        }
        private List<User> GetUsersList()
        {
            return new List<User>() { GetUser() };
        }
        private Ticket GetTicket()
        {
            return new Ticket() { Id = 1, Title = "Ticket Title", Body = "Ticket Body", Status = Status.Open, Assignee = GetUser() };
        }
        private List<Ticket> GetTicketsList()
        {
            return new List<Ticket>() { GetTicket() };
        }

        private DashboardController GetController()
        {
            return new DashboardController(_mockTicketCoordinator.Object, _mockUserCoordinator.Object);
        }

        private Mock<IUserCoordinator> _mockUserCoordinator;
        private Mock<ITicketCoordinator> _mockTicketCoordinator;
    }
}
