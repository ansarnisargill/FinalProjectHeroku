namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whenUpdatedPsychologist : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Psychologists", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Psychologists", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Psychologists", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Psychologists", "Name", c => c.String());
            AlterColumn("dbo.Psychologists", "Password", c => c.String());
            AlterColumn("dbo.Psychologists", "Username", c => c.String());
        }
    }
}
