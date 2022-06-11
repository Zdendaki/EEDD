using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class SignallerValidityPrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidTo",
                table: "Signallers",
                type: "datetime2(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFrom",
                table: "Signallers",
                type: "datetime2(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidTo",
                table: "Signallers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFrom",
                table: "Signallers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldPrecision: 0,
                oldNullable: true);
        }
    }
}
