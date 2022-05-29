using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class TrainEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainHistories");

            migrationBuilder.CreateTable(
                name: "TrainEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    EventType = table.Column<byte>(type: "tinyint", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainEvents_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainEvents_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainEvents_StationId",
                table: "TrainEvents",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainEvents_TrainId",
                table: "TrainEvents",
                column: "TrainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainEvents");

            migrationBuilder.CreateTable(
                name: "TrainHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ARoute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATrack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Arrival = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DRoute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DTrack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Delay = table.Column<short>(type: "smallint", nullable: false),
                    DelayReason = table.Column<byte>(type: "tinyint", nullable: false),
                    Departure = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Stop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainHistories_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainHistories_TrainId",
                table: "TrainHistories",
                column: "TrainId");
        }
    }
}
