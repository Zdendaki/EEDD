using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class SignallerChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Order",
                table: "Signallers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Signallers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                table: "Signallers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                table: "Signallers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Signallers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Signallers");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "Signallers");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                table: "Signallers");
        }
    }
}
