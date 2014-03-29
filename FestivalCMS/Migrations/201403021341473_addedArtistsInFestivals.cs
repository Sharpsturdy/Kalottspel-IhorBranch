namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedArtistsInFestivals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistOnFestivals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArtistID = c.Int(nullable: false),
                        FestivalID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .ForeignKey("dbo.Festivals", t => t.FestivalID, cascadeDelete: true)
                .Index(t => t.ArtistID)
                .Index(t => t.FestivalID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtistOnFestivals", "FestivalID", "dbo.Festivals");
            DropForeignKey("dbo.ArtistOnFestivals", "ArtistID", "dbo.Artists");
            DropIndex("dbo.ArtistOnFestivals", new[] { "FestivalID" });
            DropIndex("dbo.ArtistOnFestivals", new[] { "ArtistID" });
            DropTable("dbo.ArtistOnFestivals");
        }
    }
}
