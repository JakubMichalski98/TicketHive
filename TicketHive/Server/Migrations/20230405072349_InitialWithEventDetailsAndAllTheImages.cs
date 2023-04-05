using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketHive.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithEventDetailsAndAllTheImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PricePerTicket = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTickets = table.Column<int>(type: "int", nullable: false),
                    SoldTickets = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventModelUserModel",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventModelUserModel", x => new { x.EventsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_EventModelUserModel_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventModelUserModel_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "EventDetails", "EventName", "EventPlace", "EventType", "Image", "PricePerTicket", "SoldTickets", "TotalTickets" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "The chess pieces gathered 'round for a thrilling showdown! With knights charging and bishops plotting, who will reign supreme as the ultimate chess champion? Let the games begin!", "Chess Tournament", "The Basement", "Sport", "image 25.png", 200m, 30, 150 },
                    { 2, new DateTime(2024, 8, 10, 12, 30, 0, 0, DateTimeKind.Unspecified), "The legendary Bengt-Åke is back with a new thrilling Lama Race for the whole family to enjoy. Who will be the next Lama Race Master?", "Bengt-Åkes Lama Race", "Bengans Trav- & korv-service Arena", "Sport", "image 13.png", 350m, 639, 1500 },
                    { 3, new DateTime(2025, 5, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), "Get your earplugs out because your baby niece that you usually only see during Christmas is performing with her friends in this once in a lifetime concert. ", "Klass 3B`s spring concert", "Folkets Hus, Linköping", "Concert", "image 19.png", 50m, 10, 60 },
                    { 4, new DateTime(2025, 11, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), "Join Benjamin, the blogging guru, for a fun and informative session on crafting killer blog posts! With practical tips and insider secrets, you'll be a pro in no time. Bring your creativity and get ready to write!", "Benjamins Bloggskola", "Byaskolan", "Learning", "image 2.png", 199m, 1, 45 },
                    { 5, new DateTime(2025, 12, 18, 18, 0, 0, 0, DateTimeKind.Unspecified), "Coming up, coming up this holiday season with E-type, the legendary Swedish pop star, as he takes the stage for a festive concert! Sing and dance along to your favorite hits, and enjoy the magic of Christmas with this one-of-a-kind performance.", "E-types Christmas Tour", "House Arena", "Concert", "image 15.png", 650m, 2500, 14000 },
                    { 6, new DateTime(2025, 7, 13, 20, 0, 0, 0, DateTimeKind.Unspecified), "Rock out with Lisa Ajax as she puts her own spin on classic Ozzy Osbourne hits in a night of electrifying entertainment! You wn't to miss this epic cover concert.", "Lisa Ajax vs Ozzy Osbourne", "Cirkus, Stockholm", "Concert", "image 10.png", 700m, 5000, 10000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventModelUserModel_UsersId",
                table: "EventModelUserModel",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventModelUserModel");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
