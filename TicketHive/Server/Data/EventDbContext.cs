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
                Date = new DateTime(2024,06,30),
                PricePerTicket = 200,
                SoldTickets = 30,
                TotalTickets = 150,
             
            });


        }

    }
}
