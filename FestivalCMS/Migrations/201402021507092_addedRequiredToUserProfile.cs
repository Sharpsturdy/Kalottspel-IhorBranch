namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequiredToUserProfile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "FirstName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "FirstName", c => c.String());
            AlterColumn("dbo.UserProfiles", "UserName", c => c.String());
        }
    }
}
