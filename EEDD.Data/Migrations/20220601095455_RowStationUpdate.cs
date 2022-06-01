using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class RowStationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_Sig1AId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_Sig1DId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_Sig2AId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_Sig2DId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_Sig3AId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_Sig3DId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_Sig4AId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_Sig4DId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataStrings_CaptionId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_CaptionId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "CancelledA",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "CaptionId",
                table: "Rows");

            migrationBuilder.RenameColumn(
                name: "CancelledD",
                table: "Rows",
                newName: "Cancelled");

            migrationBuilder.AddColumn<byte>(
                name: "Color",
                table: "Stations",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<string>(
                name: "ExceptionsD",
                table: "Rows",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExceptionsA",
                table: "Rows",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Rows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RowDataDelays",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowId1",
                table: "RowDataDelays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RowDataSignallers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SignallerId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Changed = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowDataSignallers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RowDataSignallers_Signallers_SignallerId",
                        column: x => x.SignallerId,
                        principalTable: "Signallers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RowDataDelays_RowId1",
                table: "RowDataDelays",
                column: "RowId1");

            migrationBuilder.CreateIndex(
                name: "IX_RowDataSignallers_SignallerId",
                table: "RowDataSignallers",
                column: "SignallerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RowDataDelays_Rows_RowId1",
                table: "RowDataDelays",
                column: "RowId1",
                principalTable: "Rows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig1AId",
                table: "Rows",
                column: "Sig1AId",
                principalTable: "RowDataSignallers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig1DId",
                table: "Rows",
                column: "Sig1DId",
                principalTable: "RowDataSignallers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig2AId",
                table: "Rows",
                column: "Sig2AId",
                principalTable: "RowDataSignallers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig2DId",
                table: "Rows",
                column: "Sig2DId",
                principalTable: "RowDataSignallers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig3AId",
                table: "Rows",
                column: "Sig3AId",
                principalTable: "RowDataSignallers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig3DId",
                table: "Rows",
                column: "Sig3DId",
                principalTable: "RowDataSignallers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig4AId",
                table: "Rows",
                column: "Sig4AId",
                principalTable: "RowDataSignallers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig4DId",
                table: "Rows",
                column: "Sig4DId",
                principalTable: "RowDataSignallers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RowDataDelays_Rows_RowId1",
                table: "RowDataDelays");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig1AId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig1DId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig2AId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig2DId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig3AId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig3DId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig4AId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataSignallers_Sig4DId",
                table: "Rows");

            migrationBuilder.DropTable(
                name: "RowDataSignallers");

            migrationBuilder.DropIndex(
                name: "IX_RowDataDelays_RowId1",
                table: "RowDataDelays");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RowDataDelays");

            migrationBuilder.DropColumn(
                name: "RowId1",
                table: "RowDataDelays");

            migrationBuilder.RenameColumn(
                name: "Cancelled",
                table: "Rows",
                newName: "CancelledD");

            migrationBuilder.AlterColumn<string>(
                name: "ExceptionsD",
                table: "Rows",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExceptionsA",
                table: "Rows",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CancelledA",
                table: "Rows",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CaptionId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rows_CaptionId",
                table: "Rows",
                column: "CaptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_Sig1AId",
                table: "Rows",
                column: "Sig1AId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_Sig1DId",
                table: "Rows",
                column: "Sig1DId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_Sig2AId",
                table: "Rows",
                column: "Sig2AId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_Sig2DId",
                table: "Rows",
                column: "Sig2DId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_Sig3AId",
                table: "Rows",
                column: "Sig3AId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_Sig3DId",
                table: "Rows",
                column: "Sig3DId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_Sig4AId",
                table: "Rows",
                column: "Sig4AId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_Sig4DId",
                table: "Rows",
                column: "Sig4DId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataStrings_CaptionId",
                table: "Rows",
                column: "CaptionId",
                principalTable: "RowDataStrings",
                principalColumn: "Id");
        }
    }
}
