namespace EE5207.Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDownloadsToPublications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "Downloads", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "Downloads");
        }
    }
}
