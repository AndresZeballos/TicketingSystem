using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Models;

namespace TicketingSystem.Tests.Model
{
    [TestFixture]
    class UserTest
    {
        [Test]
        public void EncriptPasswordTest()
        {
            User user = new User() { Email = _email, Password = _pass, Name = _name};
            user.EncriptPassword();
            Assert.AreEqual(user.Password, _encryptedPass);
        }

        [SetUp]
        public void Setup()
        {
            _email = "example@domain.com";
            _pass = "theUserPass";
            _encryptedPass = "TpvNdOc62srjN77ouhgSI94NfQ2zbAbSMi9GwQG7lk0=";
            _name = "John Doe";
        }

        private string _email;
        private string _pass;
        private string _encryptedPass;
        private string _name;
        private User user;
    }
}
