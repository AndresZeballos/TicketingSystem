namespace TicketingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        Status = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                        Assignee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Assignee_Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Assignee_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        IsEnable = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.Tickets", "Assignee_Id", "dbo.Users");
            DropIndex("dbo.Tickets", new[] { "Assignee_Id" });
            DropIndex("dbo.Tickets", new[] { "Author_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Tickets");
        }
    }
}
