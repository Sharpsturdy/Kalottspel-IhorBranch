namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednullstoCategorytables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "MediaTypeID", "dbo.MediaTypes");
            DropIndex("dbo.Categories", new[] { "MediaTypeID" });
            AlterColumn("dbo.Categories", "MediaTypeID", c => c.Int());
            AlterColumn("dbo.Categories", "ContentID", c => c.Int());
            CreateIndex("dbo.Categories", "MediaTypeID");
            AddForeignKey("dbo.Categories", "MediaTypeID", "dbo.MediaTypes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "MediaTypeID", "dbo.MediaTypes");
            DropIndex("dbo.Categories", new[] { "MediaTypeID" });
            AlterColumn("dbo.Categories", "ContentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "MediaTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Categories", "MediaTypeID");
            AddForeignKey("dbo.Categories", "MediaTypeID", "dbo.MediaTypes", "ID", cascadeDelete: true);
        }
    }
}
