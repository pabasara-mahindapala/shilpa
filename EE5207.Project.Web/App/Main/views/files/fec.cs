namespace EFCodeFirstDemo.Data.Migrations
{
    public partial class DateofBirthAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateofBirth",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(
                    1, 1, 1, 0, 0, 0, 0,
                    DateTimeKind.Unspecified
                    )
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateofBirth",
                table: "Students"
            );
        }
    }
}