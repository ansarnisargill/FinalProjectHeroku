namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedACTIVEinSession : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Active", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "Active");
        }
    }
}
