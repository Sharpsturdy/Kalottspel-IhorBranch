namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedArtistsStagesEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ExtLink = c.String(),
                        MediaTypeID = c.Int(nullable: false),
                        ContentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.MediaTypeID);
            
            CreateTable(
                "dbo.ArtistsOnStages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StageID = c.Int(nullable: false),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .ForeignKey("dbo.Stages", t => t.StageID, cascadeDelete: true)
                .Index(t => t.ArtistID)
                .Index(t => t.StageID);
            
            CreateTable(
                "dbo.Stages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Place = c.String(),
                        isActive = c.Boolean(nullable: false),
                        OrderNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        StartOn = c.DateTime(nullable: false),
                        //Duration = c.Int(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        StageID = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stages", t => t.StageID, cascadeDelete: true)
                .Index(t => t.StageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artists", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.ArtistsOnStages", "StageID", "dbo.Stages");
            DropForeignKey("dbo.Events", "StageID", "dbo.Stages");
            DropForeignKey("dbo.ArtistsOnStages", "ArtistID", "dbo.Artists");
            DropIndex("dbo.Artists", new[] { "MediaTypeID" });
            DropIndex("dbo.ArtistsOnStages", new[] { "StageID" });
            DropIndex("dbo.Events", new[] { "StageID" });
            DropIndex("dbo.ArtistsOnStages", new[] { "ArtistID" });
            DropTable("dbo.Events");
            DropTable("dbo.Stages");
            DropTable("dbo.ArtistsOnStages");
            DropTable("dbo.Artists");
        }
    }
}
