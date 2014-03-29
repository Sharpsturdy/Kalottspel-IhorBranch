namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDescriptionInFestival : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Festivals", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Festivals", "Description");
        }
    }
}
