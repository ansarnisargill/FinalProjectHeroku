namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecnicreturntype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Psychologists", "RegistrationNo", c => c.Double(nullable: false));
            AlterColumn("dbo.Psychologists", "CNIC", c => c.Double(nullable: false));
            AlterColumn("dbo.Psychologists", "MobileNo", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Psychologists", "MobileNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Psychologists", "CNIC", c => c.Int(nullable: false));
            AlterColumn("dbo.Psychologists", "RegistrationNo", c => c.Int(nullable: false));
        }
    }
}
