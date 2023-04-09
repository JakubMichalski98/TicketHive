using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            UserModel? userModel = await context.Users.Include(u => u.Bookings).FirstOrDefaultAsync(u => u.Username == username);

            if (userModel != null)
            {
                return Ok(userModel);
            }
            return NotFound("User with provided username not found");
        }

    }
}
