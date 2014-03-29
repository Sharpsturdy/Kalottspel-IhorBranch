namespace FestivalCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddeisplaynamestoarticles : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Headline", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Headline", c => c.String());
        }
    }
}
