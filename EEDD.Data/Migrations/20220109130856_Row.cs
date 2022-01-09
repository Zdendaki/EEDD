using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class Row : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RowDataDelays_Rows_ArchiveId",
                table: "RowDataDelays");

            migrationBuilder.RenameColumn(
                name: "ArchiveId",
                table: "RowDataDelays",
                newName: "RowId");

            migrationBuilder.RenameIndex(
                name: "IX_RowDataDelays_ArchiveId",
                table: "RowDataDelays",
                newName: "IX_RowDataDelays_RowId");

            migrationBuilder.AddForeignKey(
                name: "FK_RowDataDelays_Rows_RowId",
                table: "RowDataDelays",
                column: "RowId",
                principalTable: "Rows",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RowDataDelays_Rows_RowId",
                table: "RowDataDelays");

            migrationBuilder.RenameColumn(
                name: "RowId",
                table: "RowDataDelays",
                newName: "ArchiveId");

            migrationBuilder.RenameIndex(
                name: "IX_RowDataDelays_RowId",
                table: "RowDataDelays",
                newName: "IX_RowDataDelays_ArchiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_RowDataDelays_Rows_ArchiveId",
                table: "RowDataDelays",
                column: "ArchiveId",
                principalTable: "Rows",
                principalColumn: "Id");
        }
    }
}
