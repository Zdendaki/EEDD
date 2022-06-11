using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class StationsSignallersBinding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignallerStation");

            migrationBuilder.AddColumn<int>(
                name: "StationId",
                table: "Signallers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Signallers_StationId",
                table: "Signallers",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Signallers_Stations_StationId",
                table: "Signallers",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signallers_Stations_StationId",
                table: "Signallers");

            migrationBuilder.DropIndex(
                name: "IX_Signallers_StationId",
                table: "Signallers");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "Signallers");

            migrationBuilder.CreateTable(
                name: "SignallerStation",
                columns: table => new
                {
                    SignallersId = table.Column<int>(type: "int", nullable: false),
                    StationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignallerStation", x => new { x.SignallersId, x.StationsId });
                    table.ForeignKey(
                        name: "FK_SignallerStation_Signallers_SignallersId",
                        column: x => x.SignallersId,
                        principalTable: "Signallers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignallerStation_Stations_StationsId",
                        column: x => x.StationsId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignallerStation_StationsId",
                table: "SignallerStation",
                column: "StationsId");
        }
    }
}
