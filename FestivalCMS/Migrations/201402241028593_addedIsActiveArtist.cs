namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsActiveArtist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "isActive");
        }
    }
}
