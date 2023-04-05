using Microsoft.EntityFrameworkCore;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Data
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<EventModel> Events { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EventModel>().HasData(new EventModel
            {
                Id = 1,
                EventName = "Chess Tournament",
                EventPlace = "The Basement",
                EventType = "Sport",
                EventDetails = "The chess pieces gathered 'round for a thrilling showdown! With knights charging and bishops plotting, who will reign supreme as the ultimate chess champion? Let the games begin!",
                Date = new DateTime(2024,06,30),
                PricePerTicket = 200,
                SoldTickets = 30,
                TotalTickets = 150,
                Image = "image 25.png"
             
            });
            modelBuilder.Entity<EventModel>().HasData(new EventModel
            {
                Id = 2,
                EventName = "Bengt-Åkes Lama Race",
                EventPlace = "Bengans Trav- & korv-service Arena",
                EventType = "Sport",
                EventDetails = "The legendary Bengt-Åke is back with a new thrilling Lama Race for the whole family to enjoy. Who will be the next Lama Race Master?",
                Date = new DateTime(2024, 08, 10, 12, 30, 00),
                PricePerTicket = 350,
                SoldTickets = 639,
                TotalTickets = 1500,
                Image = "image 13.png"

            });
            modelBuilder.Entity<EventModel>().HasData(new EventModel
            {
                Id = 3,
                EventName = "Klass 3B`s spring concert",
                EventPlace = "Folkets Hus, Linköping",
                EventType = "Concert",
                EventDetails = "Get your earplugs out because your baby niece that you usually only see during Christmas is performing with her friends in this once in a lifetime concert. ",
                Date = new DateTime(2025, 05, 07, 09, 00, 00),
                PricePerTicket = 50,
                SoldTickets = 10,
                TotalTickets = 60,
                Image = "image 19.png"

            });
            modelBuilder.Entity<EventModel>().HasData(new EventModel
            {
                Id = 4,
                EventName = "Benjamins Bloggskola",
                EventPlace = "Byaskolan",
                EventType = "Learning",
                EventDetails = "Join Benjamin, the blogging guru, for a fun and informative session on crafting killer blog posts! With practical tips and insider secrets, you'll be a pro in no time. Bring your creativity and get ready to write!",
                Date = new DateTime(2025, 11, 01, 19, 00, 00),
                PricePerTicket = 199,
                SoldTickets = 1,
                TotalTickets = 45,
                Image = "image 2.png"
            });
            modelBuilder.Entity<EventModel>().HasData(new EventModel
            {
                Id = 5,
                EventName = "E-types Christmas Tour",
                EventPlace = "House Arena",
                EventType = "Concert",
                EventDetails = "Coming up, coming up this holiday season with E-type, the legendary Swedish pop star, as he takes the stage for a festive concert! Sing and dance along to your favorite hits, and enjoy the magic of Christmas with this one-of-a-kind performance.",
                Date = new DateTime(2025, 12, 18, 18, 00, 00),
                PricePerTicket = 650,
                SoldTickets = 2500,
                TotalTickets = 14000,
                Image = "image 15.png"

            });
            modelBuilder.Entity<EventModel>().HasData(new EventModel
            {
                Id = 6,
                EventName = "Lisa Ajax vs Ozzy Osbourne",
                EventPlace = "Cirkus, Stockholm",
                EventType = "Concert",
                EventDetails = "Rock out with Lisa Ajax as she puts her own spin on classic Ozzy Osbourne hits in a night of electrifying entertainment! You wn't to miss this epic cover concert.",
                Date = new DateTime(2025, 07, 13, 20, 00, 00),
                PricePerTicket = 700,
                SoldTickets = 5000,
                TotalTickets = 10000,
                Image = "image 10.png"

            });


        }

    }
}
