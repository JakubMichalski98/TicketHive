using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketHive.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedAnEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "EventName", "EventPlace", "EventType", "Image", "PricePerTicket", "SoldTickets", "TotalTickets" },
                values: new object[] { 1, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chess Tournament", "The Basement", "Sport", null, 200m, 30, 150 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
