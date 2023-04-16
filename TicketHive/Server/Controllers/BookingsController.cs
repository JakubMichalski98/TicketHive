using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly EventDbContext context;

        public BookingsController(EventDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns All bookings including the events from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<BookingModel>>> GetEvents()
        {
            return await context.Bookings.Include(b => b.EventModel).ToListAsync();
        }

        /// <summary>
        /// Adds a booking to database
        /// </summary>
        /// <param name="bookingModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<List<BookingModel>>> AddBooking(BookingModel bookingModel)
        {
            if (bookingModel != null)
            {
                context.Bookings.Add(bookingModel);
                await context.SaveChangesAsync();

                return Ok("Booking added!");
            }
            return BadRequest("Something went wrong when adding booking");
        }

        /// <summary>
        /// Removes booking with provided id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<BookingModel>>> RemoveBooking(int id)
        {
            var bookingToRemove = await context.Bookings.FirstOrDefaultAsync(e => e.Id == id);

            if (bookingToRemove != null)
            {
                context.Bookings.Remove(bookingToRemove);
                await context.SaveChangesAsync();
            }

            return Ok(context.Events.ToListAsync());
        }
    }
}
