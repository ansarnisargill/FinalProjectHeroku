namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pSySeTtInGNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PsySettings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PsychologistID = c.Int(nullable: false),
                        DurationOfAppointment = c.Int(nullable: false),
                        StartOfDay = c.Time(nullable: false, precision: 7),
                        NumberOfDaysToShow = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Psychologists", t => t.PsychologistID, cascadeDelete: true)
                .Index(t => t.PsychologistID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PsySettings", "PsychologistID", "dbo.Psychologists");
            DropIndex("dbo.PsySettings", new[] { "PsychologistID" });
            DropTable("dbo.PsySettings");
        }
    }
}
