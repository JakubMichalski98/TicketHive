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

        /// <summary>
        /// Sends GET request to controller in order to fetch all bookings and populate Bookings list
        /// </summary>
        /// <returns></returns>
        public async Task GetBookings()
        {
            Bookings = await httpClient.GetFromJsonAsync<List<BookingModel>>("api/bookings");
        }

        /// <summary>
        /// Sends POST request to controller in order to add provided booking
        /// </summary>
        /// <param name="bookingModel"></param>
        /// <returns></returns>
        public async Task AddBooking(BookingModel bookingModel)
        {
            var result = await httpClient.PostAsJsonAsync("api/bookings", bookingModel);
        }

        /// <summary>
        /// Sends DELETE request to controller in order to remove booking with provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveBooking(int id)
        {
            var result = await httpClient.DeleteAsync($"api/Bookings/{id}");
        }
    }
}
