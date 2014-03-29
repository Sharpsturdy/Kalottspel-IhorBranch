namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequiredToUserProfile3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "UserName", c => c.String());
            AlterColumn("dbo.UserProfiles", "FirstName", c => c.String());
            AlterColumn("dbo.UserProfiles", "LastName", c => c.String());
            AlterColumn("dbo.UserProfiles", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "UserName", c => c.String(nullable: false));
        }
    }
}
