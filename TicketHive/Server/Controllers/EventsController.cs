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
    public class EventsController : ControllerBase
    {
        private readonly EventDbContext context;

        public EventsController(EventDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventModel>>> GetEvents()
        {
            return await context.Events.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<EventModel>> GetEvent(int id)
        {
            EventModel? eventModel = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (eventModel != null)
            {
                return Ok(eventModel);
            }
            return NotFound("Event with provided ID not found");
        }

        [HttpPost]
        public async Task<ActionResult<List<EventModel>>> AddEvent(EventModel eventModel)
        {
            if (eventModel != null)
            {

                context.Events.Add(eventModel);
                await context.SaveChangesAsync();

                return Ok("Event added!");
            }
            return BadRequest("Something went wrong when adding event");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<EventModel>>> UpdateAvailableEventTickets(int? eventModelId, int quantity)
        {
            if (eventModelId != null)
            {
                var foundEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == eventModelId);

                if (foundEvent != null)
                {
                    foundEvent.AvailableTickets = quantity;

                    await context.SaveChangesAsync();
                }
            }
            return BadRequest("Something went wrong when updating amount of available tickets for event");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<EventModel>>> RemoveEvent(int id)
        {
            var eventToRemove = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (eventToRemove != null)
            {
                context.Events.Remove(eventToRemove);
                await context.SaveChangesAsync();
            }

            return Ok(context.Events.ToListAsync());
        }

    }
}
