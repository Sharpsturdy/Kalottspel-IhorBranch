namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPriceToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Price");
        }
    }
}
