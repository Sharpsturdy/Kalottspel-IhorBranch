namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedordernumArtist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "OrderNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "OrderNum");
        }
    }
}
