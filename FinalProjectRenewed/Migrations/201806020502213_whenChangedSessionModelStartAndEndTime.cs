namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whenChangedSessionModelStartAndEndTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sessions", "StartingTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Sessions", "EndingTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "EndingTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Sessions", "StartingTime", c => c.DateTime(nullable: false));
        }
    }
}
