using Microsoft.AspNetCore.Mvc;
using TicketHive.Shared.Models;

namespace TicketHive.Client.Repositories
{
    public interface IBookingRepo
    {
        public Task AddBooking(BookingModel bookingModel);
        public Task GetBookings();

        public List<BookingModel> Bookings { get; set; }
    }
}
