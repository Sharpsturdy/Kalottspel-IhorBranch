namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedgalleryphotos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryPhotoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        PhotoGalleryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhotoGalleries", t => t.PhotoGalleryID, cascadeDelete: true)
                .Index(t => t.PhotoGalleryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GalleryPhotoes", "PhotoGalleryID", "dbo.PhotoGalleries");
            DropIndex("dbo.GalleryPhotoes", new[] { "PhotoGalleryID" });
            DropTable("dbo.GalleryPhotoes");
        }
    }
}
