namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFieldsInEventAndAddedArtistOnEventTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistOnEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArtistID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .Index(t => t.ArtistID)
                .Index(t => t.EventID);
            
            AddColumn("dbo.Events", "Description", c => c.String());
            AddColumn("dbo.Events", "ExtLink", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtistOnEvents", "EventID", "dbo.Events");
            DropForeignKey("dbo.ArtistOnEvents", "ArtistID", "dbo.Artists");
            DropIndex("dbo.ArtistOnEvents", new[] { "EventID" });
            DropIndex("dbo.ArtistOnEvents", new[] { "ArtistID" });
            DropColumn("dbo.Events", "ExtLink");
            DropColumn("dbo.Events", "Description");
            DropTable("dbo.ArtistOnEvents");
        }
    }
}
