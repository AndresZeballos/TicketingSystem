using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Controllers.Filters;
using TicketingSystem.Coordinators;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        ITicketCoordinator ticketCoordinator;
        IUserCoordinator userCoordinator;

        public DashboardController(ITicketCoordinator ticketCoordinator, IUserCoordinator userCoordinator)
        {
            this.ticketCoordinator = ticketCoordinator;
            this.userCoordinator = userCoordinator;
        }

        public ActionResult Index()
        {
            var userEmail = User.Identity.Name;
            var user = userCoordinator.GetUserByEmail(userEmail);
            return View(ticketCoordinator.GetTicketsByUser(user));
        }
        
        public ActionResult CreateTicket()
        {
            var userEmail = User.Identity.Name;
            var user = userCoordinator.GetUserByEmail(userEmail);
            ViewBag.Assignee_Id = new SelectList(userCoordinator.GetUsers(), "Id", "Name", user.Id);
            Ticket ticket = new Ticket { Author = user };
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTicket([Bind(Include = "Title,Body,Status,Assignee_Id")] Ticket ticket)
        {
            var userEmail = User.Identity.Name;
            var user = userCoordinator.GetUserByEmail(userEmail);
            if (ModelState.IsValid)
            {
                ticketCoordinator.Create(ticket, user);
                return RedirectToAction("Index");
            }

            ViewBag.Assignee_Id = new SelectList(userCoordinator.GetUsers(), "Id", "Name", ticket.Assignee_Id);
            return View(ticket);
        }
        
        public ActionResult EditTicket(int id)
        {
            Ticket ticket = ticketCoordinator.GetTicketsById(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.Assignee_Id = new SelectList(userCoordinator.GetUsers(), "Id", "Name", ticket.Assignee_Id);
            return View(ticket);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTicket([Bind(Include = "Id,Title,Body,Status,Assignee_Id")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticketCoordinator.UpdateTicket(ticket);
                return RedirectToAction("Index");
            }
            ViewBag.Assignee_Id = new SelectList(userCoordinator.GetUsers(), "Id", "Name", ticket.Assignee_Id);
            return View(ticket);
        }

        // GET: TicketsFoooo/Delete/5
        public ActionResult DeleteTicket(int id)
        {
            Ticket ticket = ticketCoordinator.GetTicketsById(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: TicketsFoooo/Delete/5
        [HttpPost, ActionName("DeleteTicket")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTicketConfirmed(int id)
        {
            ticketCoordinator.DeleteTicket(id);
            return RedirectToAction("Index");
        }

    }
}