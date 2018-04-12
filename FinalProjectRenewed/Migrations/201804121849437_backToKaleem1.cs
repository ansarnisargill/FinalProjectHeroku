namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backToKaleem1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Psychologists", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Psychologists", "Email");
        }
    }
}
