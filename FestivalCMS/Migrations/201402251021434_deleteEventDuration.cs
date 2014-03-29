namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteEventDuration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "Duration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Duration", c => c.Time(nullable: false, precision: 7));
        }
    }
}
