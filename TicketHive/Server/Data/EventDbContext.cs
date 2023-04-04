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
    }
}
