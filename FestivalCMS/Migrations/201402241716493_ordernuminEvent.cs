namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordernuminEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "OrderNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "OrderNum");
        }
    }
}
