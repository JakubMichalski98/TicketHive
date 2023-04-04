using System.Net.Http.Json;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public class EventRepo : IEventRepo
    {
        private readonly HttpClient httpClient;

        public List<EventModel> Events { get; set; } = new();

        public EventRepo(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task GetAllEvents()
        {
            var result = await httpClient.GetFromJsonAsync<List<EventModel>>("api/events");

            if (result != null)
            {
                Events = result;
            }
        }

        public Task<EventModel?> GetEvent(int id)
        {
            throw new NotImplementedException();
        }
        public Task AddEvent(EventModel eventToAdd)
        {
            throw new NotImplementedException();
        }
        public Task RemoveEvent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
