namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedNotification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "UserID", "dbo.Users");
            DropIndex("dbo.Notifications", new[] { "UserID" });
            AddColumn("dbo.Notifications", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Type");
            CreateIndex("dbo.Notifications", "UserID");
            AddForeignKey("dbo.Notifications", "UserID", "dbo.Users", "ID", cascadeDelete: true);
        }
    }
}
