using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Repositories
{
    public class EventRepo : IEventRepo
    {
        private readonly EventDbContext context;

        public EventRepo(EventDbContext context)
        {
            this.context = context;
        }
        public async Task<List<EventModel>> GetAllEvents()
        {
            return await context.Events.Include(e => e.Users).ToListAsync();
        }

        public async Task<EventModel?> GetEvent(int id)
        {
            EventModel? eventModel = await context.Events.Include(e => e.Users).SingleOrDefaultAsync(e => e.Id == id);

            return eventModel;
        }
        public async Task AddEvent(EventModel eventToAdd)
        {
            context.Events.Add(eventToAdd);
            await context.SaveChangesAsync();
        }

        public async Task RemoveEvent(int id)
        {
            var eventToRemove = await context.Events.FindAsync(id);

            if (eventToRemove != null)
            {
                context.Events.Remove(eventToRemove);
                await context.SaveChangesAsync();
            }
        }
    }
}
