using Microsoft.EntityFrameworkCore.Migrations;

namespace Locations.Data.Migrations
{
    public partial class LocDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationDesc",
                table: "Locations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationDesc",
                table: "Locations");
        }
    }
}
