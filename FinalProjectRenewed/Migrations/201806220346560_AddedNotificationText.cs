namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotificationText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Text");
        }
    }
}
