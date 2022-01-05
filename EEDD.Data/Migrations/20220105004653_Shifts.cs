using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class Shifts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupied",
                table: "Clients");

            migrationBuilder.AddColumn<bool>(
                name: "RowComplete",
                table: "ArchiveRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowComplete",
                table: "ArchiveRecords");

            migrationBuilder.AddColumn<bool>(
                name: "Occupied",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
