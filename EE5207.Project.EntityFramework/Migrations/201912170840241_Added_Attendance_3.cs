namespace EE5207.Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Attendance_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "Percentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "Percentage");
        }
    }
}
