namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsActiveUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "IsActive");
        }
    }
}
