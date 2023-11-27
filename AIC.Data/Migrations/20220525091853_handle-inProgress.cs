using Microsoft.EntityFrameworkCore.Migrations;

namespace AIC.Data.Migrations
{
    public partial class handleinProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InProgress",
                table: "ProfileDegrees");

            migrationBuilder.AddColumn<bool>(
                name: "InProgress",
                table: "Degrees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InProgress",
                table: "Degrees");

            migrationBuilder.AddColumn<bool>(
                name: "InProgress",
                table: "ProfileDegrees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
