namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUnknown : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "MediaTypeID", "dbo.MediaTypes");
            DropIndex("dbo.Articles", new[] { "MediaTypeID" });
            AlterColumn("dbo.Articles", "MediaTypeID", c => c.Int());
            AlterColumn("dbo.Articles", "ContentID", c => c.Int());
            CreateIndex("dbo.Articles", "MediaTypeID");
            AddForeignKey("dbo.Articles", "MediaTypeID", "dbo.MediaTypes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "MediaTypeID", "dbo.MediaTypes");
            DropIndex("dbo.Articles", new[] { "MediaTypeID" });
            AlterColumn("dbo.Articles", "ContentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Articles", "MediaTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "MediaTypeID");
            AddForeignKey("dbo.Articles", "MediaTypeID", "dbo.MediaTypes", "ID", cascadeDelete: true);
        }
    }
}
