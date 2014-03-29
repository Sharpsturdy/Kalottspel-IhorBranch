namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "TicketCode", c => c.String());
            AlterColumn("dbo.Artists", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Festivals", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Festivals", "Name", c => c.String());
            AlterColumn("dbo.Events", "Title", c => c.String());
            AlterColumn("dbo.Artists", "Name", c => c.String());
            DropColumn("dbo.Events", "TicketCode");
        }
    }
}
