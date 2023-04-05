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
            var eventToRemove = await context.Events.Include(e => e.Users).FirstOrDefaultAsync(e => e.Id == id);

            if (eventToRemove != null)
            {
                context.Events.Remove(eventToRemove);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateEvent(EventModel updatedEvent, int id)
        {
            var foundEvent = await context.Events.Include(e => e.Users).FirstOrDefaultAsync(e => e.Id == id);

            if (foundEvent != null)
            {
                foundEvent.EventName = updatedEvent.EventName;
                foundEvent.EventType = updatedEvent.EventType;
                foundEvent.EventPlace = updatedEvent.EventPlace;
                foundEvent.EventDetails = updatedEvent.EventDetails;
                foundEvent.Date = updatedEvent.Date;
                foundEvent.PricePerTicket = updatedEvent.PricePerTicket;
                foundEvent.SoldTickets = updatedEvent.SoldTickets;
                foundEvent.TotalTickets= updatedEvent.TotalTickets;
                foundEvent.Image = updatedEvent.Image;

                await context.SaveChangesAsync();
            }
        }
    }
}
