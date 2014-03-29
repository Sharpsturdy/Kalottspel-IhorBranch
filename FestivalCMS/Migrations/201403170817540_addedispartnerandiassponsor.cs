namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedispartnerandiassponsor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsPartner", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsSponsor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IsSponsor");
            DropColumn("dbo.Categories", "IsPartner");
        }
    }
}
