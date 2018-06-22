namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uPDATEEDrESULTS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "Illness", c => c.String());
            AddColumn("dbo.Results", "IsReligios", c => c.Boolean(nullable: false));
            AddColumn("dbo.Results", "Siblings", c => c.Int(nullable: false));
            AddColumn("dbo.Results", "DateOfTest", c => c.DateTime(nullable: false));
            AddColumn("dbo.Results", "EmploymentStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Results", "JobSatisfaction", c => c.Int(nullable: false));
            AddColumn("dbo.Results", "AttachmentWithFamily", c => c.Boolean(nullable: false));
            AddColumn("dbo.Results", "HomeEnvironmentStrictness", c => c.Int(nullable: false));
            AddColumn("dbo.Results", "Insomnia", c => c.Boolean(nullable: false));
            AddColumn("dbo.Results", "BDIScore", c => c.Int(nullable: false));
            DropColumn("dbo.Results", "HistoryOfIllness");
            DropColumn("dbo.Results", "Religion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Results", "Religion", c => c.String());
            AddColumn("dbo.Results", "HistoryOfIllness", c => c.String());
            DropColumn("dbo.Results", "BDIScore");
            DropColumn("dbo.Results", "Insomnia");
            DropColumn("dbo.Results", "HomeEnvironmentStrictness");
            DropColumn("dbo.Results", "AttachmentWithFamily");
            DropColumn("dbo.Results", "JobSatisfaction");
            DropColumn("dbo.Results", "EmploymentStatus");
            DropColumn("dbo.Results", "DateOfTest");
            DropColumn("dbo.Results", "Siblings");
            DropColumn("dbo.Results", "IsReligios");
            DropColumn("dbo.Results", "Illness");
        }
    }
}
