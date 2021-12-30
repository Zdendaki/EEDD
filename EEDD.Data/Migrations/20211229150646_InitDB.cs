using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    TokenIssued = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    Occupied = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trains_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteUser",
                columns: table => new
                {
                    RoutesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteUser", x => new { x.RoutesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RouteUser_Routes_RoutesId",
                        column: x => x.RoutesId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shifts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Signallers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signallers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signallers_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StationConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Interlocking = table.Column<bool>(type: "bit", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    PrimaryId = table.Column<int>(type: "int", nullable: false),
                    SecondaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StationConnections_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StationConnections_Stations_PrimaryId",
                        column: x => x.PrimaryId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StationConnections_Stations_SecondaryId",
                        column: x => x.SecondaryId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteTracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<byte>(type: "tinyint", nullable: false),
                    ConnectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteTracks_StationConnections_ConnectionId",
                        column: x => x.ConnectionId,
                        principalTable: "StationConnections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowType = table.Column<int>(type: "int", nullable: false),
                    ResponsibleUserId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: true),
                    Left = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LeftTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Right = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RightTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    ANumber = table.Column<int>(type: "int", nullable: true),
                    DNumber = table.Column<int>(type: "int", nullable: true),
                    AType = table.Column<int>(type: "int", nullable: true),
                    DType = table.Column<int>(type: "int", nullable: true),
                    ARouteId = table.Column<int>(type: "int", nullable: true),
                    DRouteId = table.Column<int>(type: "int", nullable: true),
                    AAnnounced = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DAnnounced = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    AAnnouncedTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DAnnouncedTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    APMD = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DPMD = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    APMDTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DPMDTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    AActualDep = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DActualDep = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    AActualDepTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DActualDepTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    ATrackId = table.Column<int>(type: "int", nullable: true),
                    DTrackId = table.Column<int>(type: "int", nullable: true),
                    ATrackTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DTrackTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig1A = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig2A = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig3A = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig4A = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig1D = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig2D = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig3D = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig4D = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig1ATime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig2ATime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig3ATime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig4ATime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig1DTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig2DTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig3DTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Sig4DTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Arrival = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Departure = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    ADelay = table.Column<short>(type: "smallint", nullable: true),
                    DDelay = table.Column<short>(type: "smallint", nullable: true),
                    ADeparted = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DDeparted = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    ADepartedTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DDepartedTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    ANote = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DNote = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ANoteTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DNoteTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    AApproval = table.Column<bool>(type: "bit", nullable: true),
                    DApproval = table.Column<bool>(type: "bit", nullable: true),
                    AExceptions = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DExceptions = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ASentMessages = table.Column<short>(type: "smallint", nullable: true),
                    DSentMessages = table.Column<short>(type: "smallint", nullable: true),
                    AComment = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DComment = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ACommentTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DCommentTime = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchiveRecords_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArchiveRecords_RouteTracks_ARouteId",
                        column: x => x.ARouteId,
                        principalTable: "RouteTracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchiveRecords_RouteTracks_DRouteId",
                        column: x => x.DRouteId,
                        principalTable: "RouteTracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchiveRecords_Tracks_ATrackId",
                        column: x => x.ATrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchiveRecords_Tracks_DTrackId",
                        column: x => x.DTrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchiveRecords_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArchiveRecords_Users_ResponsibleUserId",
                        column: x => x.ResponsibleUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Arrival = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Departure = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    StationTrackId = table.Column<int>(type: "int", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: false),
                    FromId = table.Column<int>(type: "int", nullable: true),
                    ToId = table.Column<int>(type: "int", nullable: true),
                    TrackId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stops_RouteTracks_FromId",
                        column: x => x.FromId,
                        principalTable: "RouteTracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stops_RouteTracks_ToId",
                        column: x => x.ToId,
                        principalTable: "RouteTracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stops_Tracks_StationTrackId",
                        column: x => x.StationTrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stops_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Stops_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveRecords_ARouteId",
                table: "ArchiveRecords",
                column: "ARouteId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveRecords_ATrackId",
                table: "ArchiveRecords",
                column: "ATrackId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveRecords_ClientId",
                table: "ArchiveRecords",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveRecords_DRouteId",
                table: "ArchiveRecords",
                column: "DRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveRecords_DTrackId",
                table: "ArchiveRecords",
                column: "DTrackId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveRecords_ResponsibleUserId",
                table: "ArchiveRecords",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveRecords_TrainId",
                table: "ArchiveRecords",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RouteId",
                table: "Clients",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteTracks_ConnectionId",
                table: "RouteTracks",
                column: "ConnectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteUser_UsersId",
                table: "RouteUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ClientId",
                table: "Shifts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Signallers_StationId",
                table: "Signallers",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_StationConnections_PrimaryId",
                table: "StationConnections",
                column: "PrimaryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StationConnections_RouteId",
                table: "StationConnections",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_StationConnections_SecondaryId",
                table: "StationConnections",
                column: "SecondaryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_ClientId",
                table: "Stations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_FromId",
                table: "Stops",
                column: "FromId",
                unique: true,
                filter: "[FromId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_StationTrackId",
                table: "Stops",
                column: "StationTrackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stops_ToId",
                table: "Stops",
                column: "ToId",
                unique: true,
                filter: "[ToId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TrackId",
                table: "Stops",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TrainId",
                table: "Stops",
                column: "TrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_StationId",
                table: "Tracks",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_Number",
                table: "Trains",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_RouteId",
                table: "Trains",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchiveRecords");

            migrationBuilder.DropTable(
                name: "RouteUser");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Signallers");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RouteTracks");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Trains");

            migrationBuilder.DropTable(
                name: "StationConnections");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
