namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSeminars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seminars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Headline = c.String(),
                        StartOn = c.DateTime(nullable: false),
                        Text = c.String(),
                        ExtLink = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Seminars");
        }
    }
}
