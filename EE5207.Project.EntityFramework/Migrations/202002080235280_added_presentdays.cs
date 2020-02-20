namespace EE5207.Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_presentdays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "PresentDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "PresentDays");
        }
    }
}
