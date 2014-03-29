namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tenamedtoBodyinArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Body", c => c.String());
            DropColumn("dbo.Articles", "ArticleText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ArticleText", c => c.String());
            DropColumn("dbo.Articles", "Body");
        }
    }
}
