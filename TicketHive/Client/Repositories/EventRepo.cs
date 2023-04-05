using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;
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
            var response = await httpClient.GetAsync($"api/events/{id}");

            if (response.IsSuccessStatusCode)
            {
                var foundEvent = await response.Content.ReadFromJsonAsync<EventModel>();
                return foundEvent;
            }
            return null;
        }
        public async Task AddEvent(EventModel eventToAdd)
        {
            var result = await httpClient.PostAsJsonAsync("api/events", eventToAdd);

            await SetEvents(result);
        }

        public async Task RemoveEvent(int id)
        {
            var result = await httpClient.DeleteAsync($"api/events{id}");
            await SetEvents(result);
        }
        
        public async Task UpdateEvent(EventModel updatedEvent)
        {
            var result = await httpClient.PutAsJsonAsync("api/events", updatedEvent);
            await SetEvents(result);
        }
        private async Task SetEvents(HttpResponseMessage? result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<EventModel>>();
            Events = response;
        }
    }
}
