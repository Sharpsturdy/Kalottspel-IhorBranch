namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTextPropertyinArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "ArticleText", c => c.String());
            DropColumn("dbo.Articles", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Text", c => c.String());
            DropColumn("dbo.Articles", "ArticleText");
        }
    }
}
