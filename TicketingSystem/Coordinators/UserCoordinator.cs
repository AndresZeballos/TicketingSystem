using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.Coordinators
{
    public class UserCoordinator : IUserCoordinator
    {
        private Model db;

        public UserCoordinator(Model model)
        {
            db = model;
        }

        public ICollection<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public User GetUser(User user)
        {
            return GetUserByEmail(user.Email);
        }
        public User GetUserById(int id)
        {
            return db.Users.Find(id);
        }
        public User GetUserByEmail(string Email)
        {
            return db.Users.Where(u => u.Email == Email).FirstOrDefault();
        }

        public void Create(User newUser)
        {
            newUser.EncriptPassword();
            db.Users.Add(newUser);
            db.SaveChanges();
        }

        public bool ValidateLogin(User user)
        {
            user.EncriptPassword();
            User dbUser = GetUserByEmail(user.Email);
            if (dbUser == null)
                return false;
            return dbUser.Password == user.Password;
        }

        public void Disable(int id)
        {
            User dbUser = GetUserById(id);
            if (dbUser != null)
            {
                dbUser.IsEnable = false;
                db.SaveChanges();
            }
        }

        public void Enable(int id)
        {
            User dbUser = GetUserById(id);
            if (dbUser != null)
            {
                dbUser.IsEnable = true;
                db.SaveChanges();
            }
        }

        public bool IsEnabled(User user)
        {
            User dbUser = GetUserByEmail(user.Email);
            if (dbUser == null)
                return false;
            return dbUser.IsEnable;
        }
    }
}