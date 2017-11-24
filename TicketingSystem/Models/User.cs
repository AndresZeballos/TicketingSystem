using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TicketingSystem.Cryptography;

namespace TicketingSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsEnable { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Ticket> ReportedTickets { get; set; }
        public virtual ICollection<Ticket> AssignedTickets { get; set; }

        public void EncriptPassword()
        {
            Password = EncryptionHelper.EncryptPass(Password);
        }
    }
}