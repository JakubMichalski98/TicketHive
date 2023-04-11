using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Shared;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly EventDbContext context;

        public UsersController(EventDbContext context)
        {
            this.context = context;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<UserModel>> GetUser(string username)
        {
            UserModel? userModel = await context.Users.Include(u => u.Bookings).ThenInclude(b => b.EventModel).FirstOrDefaultAsync(u => u.Username == username);

            if (userModel != null)
            {
                return Ok(userModel);
            }
            return NotFound("User with provided username not found");
        }

        [HttpPost]
        public async Task<ActionResult<List<UserModel>>> AddBookingToUser(BookingInfo bookingInfo)
        {
            var foundUser = context.Users.FirstOrDefault(u => u.Username == bookingInfo.User.Username);

            if (foundUser != null)
            {
                foundUser.Bookings.Add(bookingInfo.Booking);
                await context.SaveChangesAsync();
                return (Ok("Booking added to user"));
            }
            return BadRequest("Something went wrong when adding booking to user");

        }

    }
}
