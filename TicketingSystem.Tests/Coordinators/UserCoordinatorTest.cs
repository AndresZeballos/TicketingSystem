using Moq;
using NUnit.Framework;
using System.Data.Entity;
using TicketingSystem.Coordinators;
using TicketingSystem.Models;

namespace TicketingSystem.Tests.Coordinators
{
    [TestFixture]
    public class UserCoordinatorTest
    {
        [Test]
        public void CreateUser_saves_a_User_via_context()
        {
            var user = GetUser();
            var mockSet = new Mock<DbSet<User>>();

            var mockContext = new Mock<Models.Model>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var coordinator = new UserCoordinator(mockContext.Object);
            coordinator.Create(user);

            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }


        [Test]
        public void Enable_enables_the_User_via_context()
        {
            var user = GetUser();
            var mockSet = new Mock<DbSet<User>>();
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(user);

            var mockContext = new Mock<Models.Model>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var coordinator = new UserCoordinator(mockContext.Object);
            coordinator.Enable(user.Id);

            Assert.AreEqual(true, user.IsEnable);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void Disable_disables_the_User_via_context()
        {
            var user = GetUser();
            var mockSet = new Mock<DbSet<User>>();
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(user);

            var mockContext = new Mock<Models.Model>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var coordinator = new UserCoordinator(mockContext.Object);
            coordinator.Enable(user.Id);

            Assert.AreEqual(true, user.IsEnable);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        private User GetUser()
        {
            return new User() { Id = 1, Email = "example@domain.com", Password = "theUserPass", Name = "John Doe", IsEnable = true };
        }
    }
}
