namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNotificationSupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Accepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "Accepted");
        }
    }
}
