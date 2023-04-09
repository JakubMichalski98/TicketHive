using System.Net.Http;
using System.Net.Http.Json;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public class BookingRepo : IBookingRepo
    {
        private readonly HttpClient httpClient;

        public List<BookingModel> Bookings { get; set; }

        public BookingRepo(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task GetBookings()
        {
            Bookings = await httpClient.GetFromJsonAsync<List<BookingModel>>("api/bookings");
        }
        public async Task AddBooking(BookingModel bookingModel)
        {
            var result = await httpClient.PostAsJsonAsync("api/bookings", bookingModel);
        }
    }
}
