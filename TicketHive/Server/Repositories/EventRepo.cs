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

        /// <summary>
        /// Returns a list of all events from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<EventModel>> GetAllEvents()
        {
            return await context.Events.ToListAsync();
        }

        /// <summary>
        /// Returns one event with provided ID from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EventModel?> GetEvent(int id)
        {
            EventModel? eventModel = await context.Events.Include(e => e.Users).SingleOrDefaultAsync(e => e.Id == id);

            return eventModel;
        }
        /// <summary>
        /// Adds provided event to the database
        /// </summary>
        /// <param name="eventToAdd"></param>
        /// <returns></returns>
        public async Task AddEvent(EventModel eventToAdd)
        {
            context.Events.Add(eventToAdd);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Removes event with provided ID from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveEvent(int id)
        {
            var eventToRemove = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (eventToRemove != null)
            {
                context.Events.Remove(eventToRemove);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Updates an event with provided ID with the properties of provided EventModel
        /// </summary>
        /// <param name="updatedEvent"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
