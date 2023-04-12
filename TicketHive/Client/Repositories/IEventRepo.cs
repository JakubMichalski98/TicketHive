using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public interface IEventRepo
    {
        public List<EventModel> Events { get; set; }

        public Task<List<EventModel>> GetAllEvents();

        public Task<EventModel?> GetEvent(int id);

        public Task AddEvent(EventModel eventToAdd);

        public Task RemoveEvent(int id);

        public Task UpdateAvailableEventTickets(int eventModelId, int quantity);
    }
}
