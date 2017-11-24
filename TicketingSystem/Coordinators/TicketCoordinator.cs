using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.Coordinators
{
    public class TicketCoordinator : ITicketCoordinator
    {
        private Model db;

        public TicketCoordinator(Model model)
        {
            db = model;
        }

        public ICollection<Ticket> GetTickets()
        {
            return db.Tickets.ToList();
        }

        public ICollection<Ticket> GetTicketsByUser(User user)
        {
            if (user != null)
                return db.Tickets.Where(t => t.Assignee_Id == user.Id).ToList();
            return Enumerable.Empty<Ticket>().ToList();
        }

        public Ticket GetTicketsById(int id)
        {
            return db.Tickets.Find(id);
        }

        public void Create(Ticket ticket, User author)
        {
            ticket.Author = db.Users.Find(author.Id);
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }

        public void DeleteTicket(int id)
        {
            Ticket t = GetTicketsById(id);
            db.Tickets.Remove(t);
            db.SaveChanges();
        }

        public void UpdateTicket(Ticket ticket)
        {
            Ticket dbTicket = GetTicketsById(ticket.Id);
            dbTicket.Title = ticket.Title;
            dbTicket.Body = ticket.Body;
            dbTicket.Status = ticket.Status;
            dbTicket.Assignee = ticket.Assignee;
            db.SaveChanges();
        }
    }
}