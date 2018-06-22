namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotificationText1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Requests", name: "psychologist_ID", newName: "PsychologistID");
            RenameIndex(table: "dbo.Requests", name: "IX_psychologist_ID", newName: "IX_PsychologistID");
            DropColumn("dbo.Requests", "PsychologitsID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "PsychologitsID", c => c.Int());
            RenameIndex(table: "dbo.Requests", name: "IX_PsychologistID", newName: "IX_psychologist_ID");
            RenameColumn(table: "dbo.Requests", name: "PsychologistID", newName: "psychologist_ID");
        }
    }
}
