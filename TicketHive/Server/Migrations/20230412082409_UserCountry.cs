using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketHive.Server.Migrations
{
    /// <inheritdoc />
    public partial class UserCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCountry",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCountry",
                table: "Users");
        }
    }
}
