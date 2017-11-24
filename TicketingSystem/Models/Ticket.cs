using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Status Status { get; set; }
        public int Author_Id { get; set; }
        public virtual User Author { get; set; }
        public int Assignee_Id { get; set; }
        public virtual User Assignee { get; set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;

    }
}