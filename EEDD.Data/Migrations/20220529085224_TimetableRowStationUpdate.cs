using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class TimetableRowStationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "TrainEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Platform",
                table: "Tracks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "TimePenalty",
                table: "Stations",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TravelTime",
                table: "StationConnections",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "AcceptedAId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AcceptedDId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CancelledA",
                table: "Rows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CancelledD",
                table: "Rows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RowChar",
                table: "Rows",
                type: "nvarchar(1)",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Interlocking",
                table: "RouteTracks",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_AcceptedAId",
                table: "Rows",
                column: "AcceptedAId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_AcceptedDId",
                table: "Rows",
                column: "AcceptedDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_AcceptedAId",
                table: "Rows",
                column: "AcceptedAId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_AcceptedDId",
                table: "Rows",
                column: "AcceptedDId",
                principalTable: "RowDataDates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_AcceptedAId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_AcceptedDId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_AcceptedAId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_AcceptedDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "Expired",
                table: "TrainEvents");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "TimePenalty",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "TravelTime",
                table: "StationConnections");

            migrationBuilder.DropColumn(
                name: "AcceptedAId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "AcceptedDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "CancelledA",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "CancelledD",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "RowChar",
                table: "Rows");

            migrationBuilder.AlterColumn<bool>(
                name: "Interlocking",
                table: "RouteTracks",
                type: "bit",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }
    }
}
