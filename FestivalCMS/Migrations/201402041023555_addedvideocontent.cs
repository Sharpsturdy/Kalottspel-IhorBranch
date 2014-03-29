namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedvideocontent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "MediaID", "dbo.Media");
            DropIndex("dbo.Articles", new[] { "MediaID" });
            CreateTable(
                "dbo.MediaTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhotoGalleries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MediaTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.MediaTypeID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MediaTypeID = c.Int(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.MediaTypeID);
            
            CreateTable(
                "dbo.VideoLinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MediaTypeID = c.Int(nullable: false),
                        Link = c.String(),
                        Hosting = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.MediaTypeID);
            
            AddColumn("dbo.Articles", "MediaTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.Articles", "ContentID", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "MediaTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "ContentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Categories", "MediaTypeID");
            CreateIndex("dbo.Articles", "MediaTypeID");
            AddForeignKey("dbo.Categories", "MediaTypeID", "dbo.MediaTypes", "ID", cascadeDelete: false);
            AddForeignKey("dbo.Articles", "MediaTypeID", "dbo.MediaTypes", "ID", cascadeDelete: false);
            DropColumn("dbo.Articles", "MediaID");
            DropTable("dbo.Media");
        }
        
        public override void Down()
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
            DropForeignKey("dbo.Articles", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.Categories", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.VideoLinks", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.Photos", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.PhotoGalleries", "MediaTypeID", "dbo.MediaTypes");
            DropIndex("dbo.Articles", new[] { "MediaTypeID" });
            DropIndex("dbo.Categories", new[] { "MediaTypeID" });
            DropIndex("dbo.VideoLinks", new[] { "MediaTypeID" });
            DropIndex("dbo.Photos", new[] { "MediaTypeID" });
            DropIndex("dbo.PhotoGalleries", new[] { "MediaTypeID" });
            DropColumn("dbo.Categories", "ContentID");
            DropColumn("dbo.Categories", "MediaTypeID");
            DropColumn("dbo.Articles", "ContentID");
            DropColumn("dbo.Articles", "MediaTypeID");
            DropTable("dbo.VideoLinks");
            DropTable("dbo.Photos");
            DropTable("dbo.PhotoGalleries");
            DropTable("dbo.MediaTypes");
            CreateIndex("dbo.Articles", "MediaID");
            AddForeignKey("dbo.Articles", "MediaID", "dbo.Media", "ID", cascadeDelete: true);
        }
    }
}
