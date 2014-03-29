namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedOrderNumtoSeminar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seminars", "OrderNum", c => c.Int(nullable: false));
            AlterColumn("dbo.Seminars", "Headline", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Seminars", "Headline", c => c.String());
            DropColumn("dbo.Seminars", "OrderNum");
        }
    }
}
