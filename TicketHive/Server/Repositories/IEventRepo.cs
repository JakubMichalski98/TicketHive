using TicketHive.Shared.Models;

namespace TicketHive.Server.Repositories
{
    public interface IEventRepo
    {
        public Task<List<EventModel>> GetAllEvents();
        public Task<EventModel?> GetEvent(int id);
        public Task AddEvent(EventModel eventToAdd);
        public Task RemoveEvent(int id);
    }
}
