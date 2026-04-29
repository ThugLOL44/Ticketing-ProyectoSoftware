using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EventDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectors_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowIdentifier = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Available"),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    ReservedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "EventDate", "Name", "Status", "Venue" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2026, 6, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Concierto de Rock", "Active", "Estadio Luna Park" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), "test@ticketing.com", "Usuario Test", "hash_simulado" });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "Capacity", "EventId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), 50, new Guid("11111111-1111-1111-1111-111111111111"), "General", 5000m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 50, new Guid("11111111-1111-1111-1111-111111111111"), "VIP", 15000m }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Version" },
                values: new object[,]
                {
                    { new Guid("aa000000-0000-0000-0000-000000000001"), "A", 1, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000002"), "A", 2, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000003"), "A", 3, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000004"), "A", 4, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000005"), "A", 5, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000006"), "A", 6, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000007"), "A", 7, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000008"), "A", 8, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000009"), "A", 9, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000010"), "A", 10, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000011"), "B", 11, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000012"), "B", 12, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000013"), "B", 13, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000014"), "B", 14, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000015"), "B", 15, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000016"), "B", 16, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000017"), "B", 17, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000018"), "B", 18, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000019"), "B", 19, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000020"), "B", 20, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000021"), "C", 21, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000022"), "C", 22, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000023"), "C", 23, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000024"), "C", 24, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000025"), "C", 25, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000026"), "C", 26, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000027"), "C", 27, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000028"), "C", 28, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000029"), "C", 29, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000030"), "C", 30, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000031"), "D", 31, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000032"), "D", 32, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000033"), "D", 33, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000034"), "D", 34, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000035"), "D", 35, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000036"), "D", 36, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000037"), "D", 37, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000038"), "D", 38, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000039"), "D", 39, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000040"), "D", 40, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000041"), "E", 41, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000042"), "E", 42, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000043"), "E", 43, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000044"), "E", 44, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000045"), "E", 45, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000046"), "E", 46, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000047"), "E", 47, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000048"), "E", 48, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000049"), "E", 49, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("aa000000-0000-0000-0000-000000000050"), "E", 50, new Guid("22222222-2222-2222-2222-222222222222"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000001"), "A", 1, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000002"), "A", 2, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000003"), "A", 3, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000004"), "A", 4, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000005"), "A", 5, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000006"), "A", 6, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000007"), "A", 7, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000008"), "A", 8, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000009"), "A", 9, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000010"), "A", 10, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000011"), "B", 11, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000012"), "B", 12, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000013"), "B", 13, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000014"), "B", 14, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000015"), "B", 15, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000016"), "B", 16, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000017"), "B", 17, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000018"), "B", 18, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000019"), "B", 19, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000020"), "B", 20, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000021"), "C", 21, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000022"), "C", 22, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000023"), "C", 23, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000024"), "C", 24, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000025"), "C", 25, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000026"), "C", 26, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000027"), "C", 27, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000028"), "C", 28, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000029"), "C", 29, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000030"), "C", 30, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000031"), "D", 31, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000032"), "D", 32, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000033"), "D", 33, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000034"), "D", 34, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000035"), "D", 35, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000036"), "D", 36, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000037"), "D", 37, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000038"), "D", 38, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000039"), "D", 39, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000040"), "D", 40, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000041"), "E", 41, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000042"), "E", 42, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000043"), "E", 43, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000044"), "E", 44, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000045"), "E", 45, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000046"), "E", 46, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000047"), "E", 47, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000048"), "E", 48, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000049"), "E", 49, new Guid("33333333-3333-3333-3333-333333333333"), 0 },
                    { new Guid("bb000000-0000-0000-0000-000000000050"), "E", 50, new Guid("33333333-3333-3333-3333-333333333333"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SeatId",
                table: "Reservations",
                column: "SeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SectorId",
                table: "Seats",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_EventId",
                table: "Sectors",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}