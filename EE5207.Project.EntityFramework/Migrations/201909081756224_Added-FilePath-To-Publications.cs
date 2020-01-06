namespace EE5207.Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFilePathToPublications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "FilePath");
        }
    }
}
