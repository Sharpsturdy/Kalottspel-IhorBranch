namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIntDurationEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Duration");
        }
    }
}
