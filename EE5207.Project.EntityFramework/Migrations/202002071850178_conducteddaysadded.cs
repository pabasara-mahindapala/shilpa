namespace EE5207.Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conducteddaysadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "ConductedDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "ConductedDays");
        }
    }
}
