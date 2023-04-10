using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly EventDbContext context;

        public BookingsController(EventDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookingModel>>> GetEvents()
        {
            return await context.Bookings.Include(b => b.EventModel).ToListAsync();
        }

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
