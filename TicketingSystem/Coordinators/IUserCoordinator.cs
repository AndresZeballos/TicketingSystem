using System.Collections.Generic;
using TicketingSystem.Models;

namespace TicketingSystem.Coordinators
{
    public interface IUserCoordinator
    {
        void Create(User newUser);
        User GetUser(User user);
        User GetUserByEmail(string Email);
        ICollection<User> GetUsers();
        bool ValidateLogin(User user);
        bool IsEnabled(User user);
        void Enable(int id);
        void Disable(int id);
    }
}