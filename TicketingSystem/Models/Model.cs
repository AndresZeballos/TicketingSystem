namespace TicketingSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Ticket> Tickets { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.AssignedTickets)
                .WithRequired(e => e.Assignee)
                .HasForeignKey(e => e.Assignee_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ReportedTickets)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.Author_Id)
                .WillCascadeOnDelete(false);
        }
    }
}