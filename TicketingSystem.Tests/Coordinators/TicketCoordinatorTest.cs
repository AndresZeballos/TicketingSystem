using Moq;
using NUnit.Framework;
using System.Data.Entity;
using TicketingSystem.Coordinators;
using TicketingSystem.Models;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System;
using System.Linq;

namespace TicketingSystem.Tests.Coordinators
{
    [TestFixture]
    public class TicketCoordinatorTest
    {
        [Test]
        public void CreateTicket_saves_a_Ticket_via_context()
        {
            var ticket = GetTicket();
            var author = GetUser();

            var mockTicketSet = new Mock<DbSet<Ticket>>();
            var mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.Setup(m => m.Find(It.IsAny<int>())).Returns(author);

            var mockModel = new Mock<Models.Model>();
            mockModel.Setup(m => m.Tickets).Returns(mockTicketSet.Object);
            mockModel.Setup(m => m.Users).Returns(mockUserSet.Object);

            var coordinator = new TicketCoordinator(mockModel.Object);
            coordinator.Create(ticket, author);

            Assert.AreSame(ticket.Author, author);

            mockTicketSet.Verify(m => m.Add(It.IsAny<Ticket>()), Times.Once());
            mockModel.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void GetTickets_returns_the_tickets_as_list()
        {
            var tickets = GetTicketsList();

            var mockTicketSet = new Mock<IDbSet<Ticket>>();
            mockTicketSet.Setup(m => m.GetEnumerator()).Returns(tickets.GetEnumerator());

            var mockModel = new Mock<Models.Model>();
            mockModel.Setup(m => m.Tickets).Returns(mockTicketSet.Object);

            var coordinator = new TicketCoordinator(mockModel.Object);
            var result = coordinator.GetTickets();

            Assert.AreEqual(result, tickets);
        }

        [Test]
        public void GetTicketsByUser_returns_empty_list()
        {
            var tickets = GetTicketsList();

            var mockTicketSet = new Mock<IDbSet<Ticket>>();
            mockTicketSet.Setup(m => m.GetEnumerator()).Returns(tickets.GetEnumerator());

            var mockModel = new Mock<Models.Model>();
            mockModel.Setup(m => m.Tickets).Returns(mockTicketSet.Object);

            var coordinator = new TicketCoordinator(mockModel.Object);
            var result = coordinator.GetTicketsByUser(null);

            Assert.AreEqual(result, Enumerable.Empty<Ticket>());
        }

        [Test]
        public void GetTicketsById_returns_the_ticket()
        {
            var ticket = GetTicket();
            var tickets = GetTicketsList();

            var mockTicketSet = new Mock<IDbSet<Ticket>>();
            mockTicketSet.Setup(m => m.Find(It.IsAny<int>())).Returns(ticket);

            var mockModel = new Mock<Models.Model>();
            mockModel.Setup(m => m.Tickets).Returns(mockTicketSet.Object);

            var coordinator = new TicketCoordinator(mockModel.Object);
            var result = coordinator.GetTicketsById(ticket.Id);

            Assert.AreEqual(result, ticket);
        }

        [Test]
        public void DeleteTicket_remove_a_Ticket_via_context()
        {
            var ticket = GetTicket();

            var mockTicketSet = new Mock<DbSet<Ticket>>();
            mockTicketSet.Setup(m => m.Find(It.IsAny<int>())).Returns(ticket);

            var mockModel = new Mock<Models.Model>();
            mockModel.Setup(m => m.Tickets).Returns(mockTicketSet.Object);

            var coordinator = new TicketCoordinator(mockModel.Object);
            coordinator.DeleteTicket(ticket.Id);

            mockTicketSet.Verify(m => m.Remove(It.IsAny<Ticket>()), Times.Once());
            mockModel.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdateTicket_update_a_Ticket_via_context()
        {
            var oldTicket = GetTicket();
            var newTicket = GetTicketNewValues();

            var mockTicketSet = new Mock<DbSet<Ticket>>();
            mockTicketSet.Setup(m => m.Find(It.IsAny<int>())).Returns(oldTicket);

            var mockModel = new Mock<Models.Model>();
            mockModel.Setup(m => m.Tickets).Returns(mockTicketSet.Object);

            var coordinator = new TicketCoordinator(mockModel.Object);
            coordinator.UpdateTicket(newTicket);

            Assert.AreEqual(oldTicket.Title, newTicket.Title);
            Assert.AreEqual(oldTicket.Body, newTicket.Body);
            Assert.AreEqual(oldTicket.Status, newTicket.Status);
            Assert.AreEqual(oldTicket.Author, newTicket.Author);

            mockModel.Verify(m => m.SaveChanges(), Times.Once());
        }

        private User GetUser()
        {
            return new User() { Id = 1, Email = "example@domain.com", Password = "theUserPass", Name = "John Doe" };
        }
        private Ticket GetTicket()
        {
            return new Ticket() { Id = 1, Title = "Ticket Title", Body = "Ticket Body", Status = Status.Open, Assignee = GetUser() };
        }
        private Ticket GetTicketNewValues()
        {
            return new Ticket() { Id = 1, Title = "Ticket Title New", Body = "Ticket Body New", Status = Status.Closed, Assignee = GetUser() };
        }
        private List<Ticket> GetTicketsList()
        {
            return new List<Ticket>() { GetTicket() };
        }
    }
    
}
