using System.ComponentModel;
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
            var response = await httpClient.GetAsync("/api/events");

            if (response.IsSuccessStatusCode)
            {
                List<EventModel>? events = await response.Content.ReadFromJsonAsync<List<EventModel>>();
                Events = events;
            }
        }

        public async Task<EventModel?> GetEvent(int id)
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
