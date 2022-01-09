using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerData.Migrations
{
    public partial class ArchiveTrain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Clients_ClientId",
                table: "ArchiveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_RouteTracks_ARouteId",
                table: "ArchiveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_RouteTracks_DRouteId",
                table: "ArchiveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Stations_StationId",
                table: "ArchiveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Tracks_ATrackId",
                table: "ArchiveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Tracks_DTrackId",
                table: "ArchiveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Trains_TrainId",
                table: "ArchiveRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ArchiveRecords_Users_ResponsibleUserId",
                table: "ArchiveRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchiveRecords",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "AActualDep",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "AActualDepTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "AAnnounced",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "AAnnouncedTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "AComment",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "ACommentTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "ADeparted",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "ADepartedTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "AExceptions",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "ANote",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "ANoteTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "APMD",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "APMDTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "ATrackTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Arrival",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DActualDep",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DActualDepTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DAnnounced",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DAnnouncedTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DComment",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DCommentTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DDeparted",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DDepartedTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DExceptions",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DNote",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DNoteTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DPMD",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DPMDTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DTrackTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Departure",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "LeftTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "RightTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig1A",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig1ATime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig1D",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig1DTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig2A",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig2ATime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig2D",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig2DTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig3A",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig3ATime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig3D",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig3DTime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig4A",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig4ATime",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig4D",
                table: "ArchiveRecords");

            migrationBuilder.DropColumn(
                name: "Sig4DTime",
                table: "ArchiveRecords");

            migrationBuilder.RenameTable(
                name: "ArchiveRecords",
                newName: "Rows");

            migrationBuilder.RenameColumn(
                name: "Right",
                table: "Rows",
                newName: "ExceptionsD");

            migrationBuilder.RenameColumn(
                name: "Left",
                table: "Rows",
                newName: "ExceptionsA");

            migrationBuilder.RenameColumn(
                name: "DType",
                table: "Rows",
                newName: "TypeD");

            migrationBuilder.RenameColumn(
                name: "DTrackId",
                table: "Rows",
                newName: "TrackDId");

            migrationBuilder.RenameColumn(
                name: "DSentMessages",
                table: "Rows",
                newName: "SentMessagesD");

            migrationBuilder.RenameColumn(
                name: "DRouteId",
                table: "Rows",
                newName: "TrackAId");

            migrationBuilder.RenameColumn(
                name: "DNumber",
                table: "Rows",
                newName: "Sig4DId");

            migrationBuilder.RenameColumn(
                name: "DApproval",
                table: "Rows",
                newName: "ApprovalD");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Rows",
                newName: "Sig4AId");

            migrationBuilder.RenameColumn(
                name: "AType",
                table: "Rows",
                newName: "TypeA");

            migrationBuilder.RenameColumn(
                name: "ATrackId",
                table: "Rows",
                newName: "Sig3DId");

            migrationBuilder.RenameColumn(
                name: "ASentMessages",
                table: "Rows",
                newName: "SentMessagesA");

            migrationBuilder.RenameColumn(
                name: "ARouteId",
                table: "Rows",
                newName: "Sig3AId");

            migrationBuilder.RenameColumn(
                name: "ANumber",
                table: "Rows",
                newName: "Sig2DId");

            migrationBuilder.RenameColumn(
                name: "AApproval",
                table: "Rows",
                newName: "ApprovalA");

            migrationBuilder.RenameIndex(
                name: "IX_ArchiveRecords_TrainId",
                table: "Rows",
                newName: "IX_Rows_TrainId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchiveRecords_StationId",
                table: "Rows",
                newName: "IX_Rows_StationId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchiveRecords_ResponsibleUserId",
                table: "Rows",
                newName: "IX_Rows_ResponsibleUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchiveRecords_DTrackId",
                table: "Rows",
                newName: "IX_Rows_TrackDId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchiveRecords_DRouteId",
                table: "Rows",
                newName: "IX_Rows_TrackAId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchiveRecords_ClientId",
                table: "Rows",
                newName: "IX_Rows_Sig4AId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchiveRecords_ATrackId",
                table: "Rows",
                newName: "IX_Rows_Sig3DId");

            migrationBuilder.RenameIndex(
                name: "IX_ArchiveRecords_ARouteId",
                table: "Rows",
                newName: "IX_Rows_Sig3AId");

            migrationBuilder.AddColumn<int>(
                name: "APMDId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActualDepAId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActualDepDId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnnouncedAId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnnouncedDId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ArrivalId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CaptionId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentAId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentDId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DPMDId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartedAId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartedDId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartureId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoteAId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoteDId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberA",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberD",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouteA",
                table: "Rows",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouteD",
                table: "Rows",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sig1AId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sig1DId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sig2AId",
                table: "Rows",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rows",
                table: "Rows",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RowDataAcceptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    Changed = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    State = table.Column<byte>(type: "tinyint", nullable: false),
                    Accepted = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowDataAcceptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RowDataDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false),
                    Changed = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowDataDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RowDataDelays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<byte>(type: "tinyint", nullable: false),
                    Minutes = table.Column<short>(type: "smallint", nullable: false),
                    TrainNumber = table.Column<int>(type: "int", nullable: true),
                    ArchiveId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowDataDelays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RowDataDelays_Rows_ArchiveId",
                        column: x => x.ArchiveId,
                        principalTable: "Rows",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RowDataStrings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Changed = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowDataStrings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RowDataTracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Track = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupied = table.Column<bool>(type: "bit", nullable: false),
                    Changed = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowDataTracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrival = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    Departure = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    ARoute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATrack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DTrack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DRoute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelayReason = table.Column<byte>(type: "tinyint", nullable: false),
                    Delay = table.Column<short>(type: "smallint", nullable: false),
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
                name: "IX_Rows_ActualDepAId",
                table: "Rows",
                column: "ActualDepAId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_ActualDepDId",
                table: "Rows",
                column: "ActualDepDId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_AnnouncedAId",
                table: "Rows",
                column: "AnnouncedAId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_AnnouncedDId",
                table: "Rows",
                column: "AnnouncedDId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_APMDId",
                table: "Rows",
                column: "APMDId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_ArrivalId",
                table: "Rows",
                column: "ArrivalId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_CaptionId",
                table: "Rows",
                column: "CaptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_CommentAId",
                table: "Rows",
                column: "CommentAId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_CommentDId",
                table: "Rows",
                column: "CommentDId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_DepartedAId",
                table: "Rows",
                column: "DepartedAId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_DepartedDId",
                table: "Rows",
                column: "DepartedDId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_DepartureId",
                table: "Rows",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_DPMDId",
                table: "Rows",
                column: "DPMDId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_MessageId",
                table: "Rows",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_NoteAId",
                table: "Rows",
                column: "NoteAId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_NoteDId",
                table: "Rows",
                column: "NoteDId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_Sig1AId",
                table: "Rows",
                column: "Sig1AId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_Sig1DId",
                table: "Rows",
                column: "Sig1DId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_Sig2AId",
                table: "Rows",
                column: "Sig2AId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_Sig2DId",
                table: "Rows",
                column: "Sig2DId");

            migrationBuilder.CreateIndex(
                name: "IX_Rows_Sig4DId",
                table: "Rows",
                column: "Sig4DId");

            migrationBuilder.CreateIndex(
                name: "IX_RowDataDelays_ArchiveId",
                table: "RowDataDelays",
                column: "ArchiveId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainHistories_TrainId",
                table: "TrainHistories",
                column: "TrainId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataAcceptions_AnnouncedDId",
                table: "Rows",
                column: "AnnouncedDId",
                principalTable: "RowDataAcceptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_ActualDepAId",
                table: "Rows",
                column: "ActualDepAId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_ActualDepDId",
                table: "Rows",
                column: "ActualDepDId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_AnnouncedAId",
                table: "Rows",
                column: "AnnouncedAId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_APMDId",
                table: "Rows",
                column: "APMDId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_ArrivalId",
                table: "Rows",
                column: "ArrivalId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_DepartedAId",
                table: "Rows",
                column: "DepartedAId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_DepartedDId",
                table: "Rows",
                column: "DepartedDId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_DepartureId",
                table: "Rows",
                column: "DepartureId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataDates_DPMDId",
                table: "Rows",
                column: "DPMDId",
                principalTable: "RowDataDates",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataStrings_CommentAId",
                table: "Rows",
                column: "CommentAId",
                principalTable: "RowDataStrings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataStrings_CommentDId",
                table: "Rows",
                column: "CommentDId",
                principalTable: "RowDataStrings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataStrings_MessageId",
                table: "Rows",
                column: "MessageId",
                principalTable: "RowDataStrings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataStrings_NoteAId",
                table: "Rows",
                column: "NoteAId",
                principalTable: "RowDataStrings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataStrings_NoteDId",
                table: "Rows",
                column: "NoteDId",
                principalTable: "RowDataStrings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataTracks_TrackAId",
                table: "Rows",
                column: "TrackAId",
                principalTable: "RowDataTracks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_RowDataTracks_TrackDId",
                table: "Rows",
                column: "TrackDId",
                principalTable: "RowDataTracks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_Stations_StationId",
                table: "Rows",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_Trains_TrainId",
                table: "Rows",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rows_Users_ResponsibleUserId",
                table: "Rows",
                column: "ResponsibleUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataAcceptions_AnnouncedDId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_ActualDepAId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_ActualDepDId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_AnnouncedAId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_APMDId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_ArrivalId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_DepartedAId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_DepartedDId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_DepartureId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataDates_DPMDId",
                table: "Rows");

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

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataStrings_CommentAId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataStrings_CommentDId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataStrings_MessageId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataStrings_NoteAId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataStrings_NoteDId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataTracks_TrackAId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_RowDataTracks_TrackDId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_Stations_StationId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_Trains_TrainId",
                table: "Rows");

            migrationBuilder.DropForeignKey(
                name: "FK_Rows_Users_ResponsibleUserId",
                table: "Rows");

            migrationBuilder.DropTable(
                name: "RowDataAcceptions");

            migrationBuilder.DropTable(
                name: "RowDataDates");

            migrationBuilder.DropTable(
                name: "RowDataDelays");

            migrationBuilder.DropTable(
                name: "RowDataStrings");

            migrationBuilder.DropTable(
                name: "RowDataTracks");

            migrationBuilder.DropTable(
                name: "TrainHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rows",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_ActualDepAId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_ActualDepDId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_AnnouncedAId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_AnnouncedDId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_APMDId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_ArrivalId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_CaptionId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_CommentAId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_CommentDId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_DepartedAId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_DepartedDId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_DepartureId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_DPMDId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_MessageId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_NoteAId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_NoteDId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_Sig1AId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_Sig1DId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_Sig2AId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_Sig2DId",
                table: "Rows");

            migrationBuilder.DropIndex(
                name: "IX_Rows_Sig4DId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "APMDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "ActualDepAId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "ActualDepDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "AnnouncedAId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "AnnouncedDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "ArrivalId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "CaptionId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "CommentAId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "CommentDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "DPMDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "DepartedAId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "DepartedDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "DepartureId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "NoteAId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "NoteDId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "NumberA",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "NumberD",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "RouteA",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "RouteD",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "Sig1AId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "Sig1DId",
                table: "Rows");

            migrationBuilder.DropColumn(
                name: "Sig2AId",
                table: "Rows");

            migrationBuilder.RenameTable(
                name: "Rows",
                newName: "ArchiveRecords");

            migrationBuilder.RenameColumn(
                name: "TypeD",
                table: "ArchiveRecords",
                newName: "DType");

            migrationBuilder.RenameColumn(
                name: "TypeA",
                table: "ArchiveRecords",
                newName: "AType");

            migrationBuilder.RenameColumn(
                name: "TrackDId",
                table: "ArchiveRecords",
                newName: "DTrackId");

            migrationBuilder.RenameColumn(
                name: "TrackAId",
                table: "ArchiveRecords",
                newName: "DRouteId");

            migrationBuilder.RenameColumn(
                name: "Sig4DId",
                table: "ArchiveRecords",
                newName: "DNumber");

            migrationBuilder.RenameColumn(
                name: "Sig4AId",
                table: "ArchiveRecords",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "Sig3DId",
                table: "ArchiveRecords",
                newName: "ATrackId");

            migrationBuilder.RenameColumn(
                name: "Sig3AId",
                table: "ArchiveRecords",
                newName: "ARouteId");

            migrationBuilder.RenameColumn(
                name: "Sig2DId",
                table: "ArchiveRecords",
                newName: "ANumber");

            migrationBuilder.RenameColumn(
                name: "SentMessagesD",
                table: "ArchiveRecords",
                newName: "DSentMessages");

            migrationBuilder.RenameColumn(
                name: "SentMessagesA",
                table: "ArchiveRecords",
                newName: "ASentMessages");

            migrationBuilder.RenameColumn(
                name: "ExceptionsD",
                table: "ArchiveRecords",
                newName: "Right");

            migrationBuilder.RenameColumn(
                name: "ExceptionsA",
                table: "ArchiveRecords",
                newName: "Left");

            migrationBuilder.RenameColumn(
                name: "ApprovalD",
                table: "ArchiveRecords",
                newName: "DApproval");

            migrationBuilder.RenameColumn(
                name: "ApprovalA",
                table: "ArchiveRecords",
                newName: "AApproval");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_TrainId",
                table: "ArchiveRecords",
                newName: "IX_ArchiveRecords_TrainId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_TrackDId",
                table: "ArchiveRecords",
                newName: "IX_ArchiveRecords_DTrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_TrackAId",
                table: "ArchiveRecords",
                newName: "IX_ArchiveRecords_DRouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_StationId",
                table: "ArchiveRecords",
                newName: "IX_ArchiveRecords_StationId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_Sig4AId",
                table: "ArchiveRecords",
                newName: "IX_ArchiveRecords_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_Sig3DId",
                table: "ArchiveRecords",
                newName: "IX_ArchiveRecords_ATrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_Sig3AId",
                table: "ArchiveRecords",
                newName: "IX_ArchiveRecords_ARouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Rows_ResponsibleUserId",
                table: "ArchiveRecords",
                newName: "IX_ArchiveRecords_ResponsibleUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "AActualDep",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AActualDepTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AAnnounced",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AAnnouncedTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AComment",
                table: "ArchiveRecords",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ACommentTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ADeparted",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ADepartedTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AExceptions",
                table: "ArchiveRecords",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ANote",
                table: "ArchiveRecords",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ANoteTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "APMD",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "APMDTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ATrackTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Arrival",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DActualDep",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DActualDepTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DAnnounced",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DAnnouncedTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DComment",
                table: "ArchiveRecords",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DCommentTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DDeparted",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DDepartedTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DExceptions",
                table: "ArchiveRecords",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DNote",
                table: "ArchiveRecords",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DNoteTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DPMD",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DPMDTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DTrackTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Departure",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeftTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RightTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig1A",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig1ATime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig1D",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig1DTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig2A",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig2ATime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig2D",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig2DTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig3A",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig3ATime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig3D",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig3DTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig4A",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig4ATime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig4D",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sig4DTime",
                table: "ArchiveRecords",
                type: "datetime2(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchiveRecords",
                table: "ArchiveRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Clients_ClientId",
                table: "ArchiveRecords",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_RouteTracks_ARouteId",
                table: "ArchiveRecords",
                column: "ARouteId",
                principalTable: "RouteTracks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_RouteTracks_DRouteId",
                table: "ArchiveRecords",
                column: "DRouteId",
                principalTable: "RouteTracks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Stations_StationId",
                table: "ArchiveRecords",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Tracks_ATrackId",
                table: "ArchiveRecords",
                column: "ATrackId",
                principalTable: "Tracks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Tracks_DTrackId",
                table: "ArchiveRecords",
                column: "DTrackId",
                principalTable: "Tracks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Trains_TrainId",
                table: "ArchiveRecords",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArchiveRecords_Users_ResponsibleUserId",
                table: "ArchiveRecords",
                column: "ResponsibleUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
