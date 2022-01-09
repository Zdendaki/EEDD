using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class ArchiveStation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Clients_ClientId",
                table: "ArchiveRecords");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ArchiveRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StationId",
                table: "ArchiveRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveRecords_StationId",
                table: "ArchiveRecords",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Clients_ClientId",
                table: "ArchiveRecords",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Stations_StationId",
                table: "ArchiveRecords",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Clients_ClientId",
                table: "ArchiveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Stations_StationId",
                table: "ArchiveRecords");

            migrationBuilder.DropIndex(
                name: "IX_ArchiveRecords_StationId",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "ArchiveRecords");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ArchiveRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Clients_ClientId",
                table: "ArchiveRecords",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
