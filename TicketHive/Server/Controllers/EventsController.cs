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
        public async Task<ActionResult<List<EventModel>>> UpdateEvent(EventModel eventModel, int id)
        {
            if (eventModel != null)
            {
                var foundEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

                if (foundEvent != null)
                {
                    foundEvent.EventName = eventModel.EventName;
                    foundEvent.EventType = eventModel.EventType;
                    foundEvent.EventPlace = eventModel.EventPlace;
                    foundEvent.EventDetails = eventModel.EventDetails;
                    foundEvent.Date = eventModel.Date;
                    foundEvent.PricePerTicket = eventModel.PricePerTicket;
                    foundEvent.AvailableTickets = eventModel.AvailableTickets;
                    foundEvent.TotalTickets = eventModel.TotalTickets;
                    foundEvent.Image = eventModel.Image;

                    await context.SaveChangesAsync();
                }
            }
            return BadRequest("Something went wrong when updating event");
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
