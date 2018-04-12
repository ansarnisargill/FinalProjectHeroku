namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backToKaleem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Psychologists", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Psychologists", "Email", c => c.String());
        }
    }
}
