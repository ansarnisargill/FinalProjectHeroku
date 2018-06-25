namespace FinalProjectRenewed.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPhotoSignUp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsPhotoSignup", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsPhotoSignup");
        }
    }
}
