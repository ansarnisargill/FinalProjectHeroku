namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(nullable: false),
                        UserID = c.Int(),
                        PsychologistID = c.Int(),
                        SessionID = c.Int(),
                        Rating = c.Int(nullable: false),
                        ResultID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Psychologists", t => t.PsychologistID)
                .ForeignKey("dbo.Results", t => t.ResultID)
                .ForeignKey("dbo.Sessions", t => t.SessionID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PsychologistID)
                .Index(t => t.SessionID)
                .Index(t => t.ResultID);
            
            CreateTable(
                "dbo.Psychologists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        ImageAddress = c.String(),
                        PsyTypeID = c.Int(nullable: false),
                        Education = c.String(),
                        Experience = c.Int(nullable: false),
                        RegistrationNo = c.Double(nullable: false),
                        CNIC = c.Double(nullable: false),
                        MobileNo = c.Double(nullable: false),
                        Sex = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PsyTypes", t => t.PsyTypeID, cascadeDelete: true)
                .Index(t => t.PsyTypeID);
            
            CreateTable(
                "dbo.PsyTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        BirthOrder = c.Int(nullable: false),
                        Illness = c.String(),
                        Education = c.String(),
                        IsReligios = c.Boolean(nullable: false),
                        SexuallyActive = c.Boolean(nullable: false),
                        Siblings = c.Int(nullable: false),
                        DateOfTest = c.DateTime(nullable: false),
                        EmploymentStatus = c.Boolean(nullable: false),
                        JobSatisfaction = c.Int(nullable: false),
                        AttachmentWithFamily = c.Boolean(nullable: false),
                        HomeEnvironmentStrictness = c.Int(nullable: false),
                        Insomnia = c.Boolean(nullable: false),
                        BDIScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsMarried = c.Boolean(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Sex = c.Boolean(nullable: false),
                        City = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        fbid = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SessionDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        StartingTime = c.Time(nullable: false, precision: 7),
                        EndingTime = c.Time(nullable: false, precision: 7),
                        PsychologistID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Psychologists", t => t.PsychologistID, cascadeDelete: true)
                .Index(t => t.PsychologistID);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PsychologitsID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        AppointmentID = c.Int(nullable: false),
                        Description = c.String(),
                        psychologist_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Appointments", t => t.AppointmentID, cascadeDelete: true)
                .ForeignKey("dbo.Psychologists", t => t.psychologist_ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.AppointmentID)
                .Index(t => t.psychologist_ID);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Link = c.String(),
                        Text = c.String(),
                        UserID = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SessionID = c.Int(),
                        PsychologistID = c.Int(),
                        UserID = c.Int(),
                        ResultID = c.Int(),
                        Accepted = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Psychologists", t => t.PsychologistID)
                .ForeignKey("dbo.Results", t => t.ResultID)
                .ForeignKey("dbo.Sessions", t => t.SessionID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.SessionID)
                .Index(t => t.PsychologistID)
                .Index(t => t.UserID)
                .Index(t => t.ResultID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "UserID", "dbo.Users");
            DropForeignKey("dbo.Requests", "SessionID", "dbo.Sessions");
            DropForeignKey("dbo.Requests", "ResultID", "dbo.Results");
            DropForeignKey("dbo.Requests", "PsychologistID", "dbo.Psychologists");
            DropForeignKey("dbo.Histories", "UserID", "dbo.Users");
            DropForeignKey("dbo.Histories", "psychologist_ID", "dbo.Psychologists");
            DropForeignKey("dbo.Histories", "AppointmentID", "dbo.Appointments");
            DropForeignKey("dbo.Appointments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Appointments", "SessionID", "dbo.Sessions");
            DropForeignKey("dbo.Sessions", "PsychologistID", "dbo.Psychologists");
            DropForeignKey("dbo.Appointments", "ResultID", "dbo.Results");
            DropForeignKey("dbo.Results", "UserID", "dbo.Users");
            DropForeignKey("dbo.Appointments", "PsychologistID", "dbo.Psychologists");
            DropForeignKey("dbo.Psychologists", "PsyTypeID", "dbo.PsyTypes");
            DropIndex("dbo.Requests", new[] { "ResultID" });
            DropIndex("dbo.Requests", new[] { "UserID" });
            DropIndex("dbo.Requests", new[] { "PsychologistID" });
            DropIndex("dbo.Requests", new[] { "SessionID" });
            DropIndex("dbo.Histories", new[] { "psychologist_ID" });
            DropIndex("dbo.Histories", new[] { "AppointmentID" });
            DropIndex("dbo.Histories", new[] { "UserID" });
            DropIndex("dbo.Sessions", new[] { "PsychologistID" });
            DropIndex("dbo.Results", new[] { "UserID" });
            DropIndex("dbo.Psychologists", new[] { "PsyTypeID" });
            DropIndex("dbo.Appointments", new[] { "ResultID" });
            DropIndex("dbo.Appointments", new[] { "SessionID" });
            DropIndex("dbo.Appointments", new[] { "PsychologistID" });
            DropIndex("dbo.Appointments", new[] { "UserID" });
            DropTable("dbo.Requests");
            DropTable("dbo.Notifications");
            DropTable("dbo.Histories");
            DropTable("dbo.Sessions");
            DropTable("dbo.Users");
            DropTable("dbo.Results");
            DropTable("dbo.PsyTypes");
            DropTable("dbo.Psychologists");
            DropTable("dbo.Appointments");
        }
    }
}
