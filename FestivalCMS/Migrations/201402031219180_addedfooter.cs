namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfooter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Footers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        PostAddress = c.String(),
                        LegalAddress = c.String(),
                        Phone = c.String(),
                        COntactPhone = c.String(),
                        CellPhone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        SupportEmail = c.String(),
                        GoogleMapIFrameLink = c.String(),
                        FacebookLink = c.String(),
                        ExternalLink = c.String(),
                        SocialLink = c.String(),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Footers");
        }
    }
}
