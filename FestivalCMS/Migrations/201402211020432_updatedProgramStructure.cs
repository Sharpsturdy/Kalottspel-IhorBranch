namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedProgramStructure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistsOnStages", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.Events", "StageID", "dbo.Stages");
            DropForeignKey("dbo.ArtistsOnStages", "StageID", "dbo.Stages");
            DropIndex("dbo.ArtistsOnStages", new[] { "ArtistID" });
            DropIndex("dbo.Events", new[] { "StageID" });
            DropIndex("dbo.ArtistsOnStages", new[] { "StageID" });
            CreateTable(
                "dbo.Festivals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        From = c.DateTime(nullable: false),
                        Untill = c.DateTime(nullable: false),
                        OrderNum = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Events", "Stage", c => c.String());
            AddColumn("dbo.Events", "FestivalID", c => c.Int());
            CreateIndex("dbo.Events", "FestivalID");
            AddForeignKey("dbo.Events", "FestivalID", "dbo.Festivals", "ID");
            DropColumn("dbo.Events", "StageID");
            DropTable("dbo.ArtistsOnStages");
            DropTable("dbo.Stages");
        }
        
        public override void Down()
        {
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
                "dbo.ArtistsOnStages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StageID = c.Int(nullable: false),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Events", "StageID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Events", "FestivalID", "dbo.Festivals");
            DropIndex("dbo.Events", new[] { "FestivalID" });
            DropColumn("dbo.Events", "FestivalID");
            DropColumn("dbo.Events", "Stage");
            DropTable("dbo.Festivals");
            CreateIndex("dbo.ArtistsOnStages", "StageID");
            CreateIndex("dbo.Events", "StageID");
            CreateIndex("dbo.ArtistsOnStages", "ArtistID");
            AddForeignKey("dbo.ArtistsOnStages", "StageID", "dbo.Stages", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Events", "StageID", "dbo.Stages", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ArtistsOnStages", "ArtistID", "dbo.Artists", "ID", cascadeDelete: true);
        }
    }
}
