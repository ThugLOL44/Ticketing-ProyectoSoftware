using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSectorPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("01222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("01333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("02222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("02333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("03222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("03333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("04222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("04333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("05222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("05333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("06222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("06333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("07222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("07333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("08222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("08333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("09222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 60000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("09333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 90000m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("01222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("01333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("02222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("02333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("03222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("03333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("04222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("04333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("05222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("05333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("06222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("06333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("07222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("07333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("08222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("08333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("09222222-2222-2222-2222-222222222222"),
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Sectors",
                keyColumn: "Id",
                keyValue: new Guid("09333333-3333-3333-3333-333333333333"),
                column: "Price",
                value: 15000m);
        }
    }
}
