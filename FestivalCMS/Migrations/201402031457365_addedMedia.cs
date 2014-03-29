namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedMedia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Articles", "MediaID", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "MediaID");
            AddForeignKey("dbo.Articles", "MediaID", "dbo.Media", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "MediaID", "dbo.Media");
            DropIndex("dbo.Articles", new[] { "MediaID" });
            DropColumn("dbo.Articles", "MediaID");
            DropTable("dbo.Media");
        }
    }
}
