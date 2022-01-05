using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class Timetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimetableArrival",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "TimetableDeparture",
                table: "Stops");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "TimetableArrival",
                table: "Stops",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "TimetableDeparture",
                table: "Stops",
                type: "smallint",
                nullable: true);
        }
    }
}
