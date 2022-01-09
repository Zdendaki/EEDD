using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class StationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interlocking",
                table: "StationConnections");

            migrationBuilder.RenameColumn(
                name: "Secured",
                table: "RouteTracks",
                newName: "Interlocking");

            migrationBuilder.RenameColumn(
                name: "MinimalDelay",
                table: "RouteTracks",
                newName: "MinimumInterval");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinimumInterval",
                table: "RouteTracks",
                newName: "MinimalDelay");

            migrationBuilder.RenameColumn(
                name: "Interlocking",
                table: "RouteTracks",
                newName: "Secured");

            migrationBuilder.AddColumn<bool>(
                name: "Interlocking",
                table: "StationConnections",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
