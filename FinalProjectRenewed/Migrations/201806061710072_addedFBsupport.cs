namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFBsupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "fbid", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "fbid");
        }
    }
}
