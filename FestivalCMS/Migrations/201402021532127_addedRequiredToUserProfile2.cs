namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRequiredToUserProfile2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.UserProfiles", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "Email", c => c.String());
            AlterColumn("dbo.UserProfiles", "LastName", c => c.String());
        }
    }
}
