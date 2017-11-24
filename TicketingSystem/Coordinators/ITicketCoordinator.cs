using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Models;

namespace TicketingSystem.Coordinators
{
    public interface ITicketCoordinator
    {
        void Create(Ticket ticket, User author);
        void DeleteTicket(int id);
        ICollection<Ticket> GetTickets();
        Ticket GetTicketsById(int id);
        ICollection<Ticket> GetTicketsByUser(User user);
        void UpdateTicket(Ticket ticket);
    }
}