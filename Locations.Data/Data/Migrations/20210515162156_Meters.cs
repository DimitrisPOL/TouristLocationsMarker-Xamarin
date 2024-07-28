using Microsoft.EntityFrameworkCore.Migrations;

namespace Locations.Data.Migrations
{
    public partial class Meters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MeterToNotify",
                table: "Locations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeterToNotify",
                table: "Locations");
        }
    }
}
