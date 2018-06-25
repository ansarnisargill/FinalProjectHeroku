namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAppointmentMiss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Missed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Missed");
        }
    }
}
