using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
        private static double exchangeRate;

        public EventsController(EventDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventModel>>> GetEvents()
        {

           return await context.Events.ToListAsync();

        }

        [HttpGet("views")]
        public async Task<ActionResult<List<EventViewModel>>> GetEventViews()
        {
            List<EventModel> events = await context.Events.ToListAsync();
            List<EventViewModel> eventViews = new();

            foreach (EventModel eventModel in events)
            {
                EventViewModel eventViewModel = new()
                {
                    Id = eventModel.Id,
                    EventName = eventModel.EventName,
                    EventType = eventModel.EventType,
                    EventPlace = eventModel.EventPlace,
                    EventDetails = eventModel.EventDetails,
                    Date = eventModel.Date,
                    PricePerTicket = eventModel.PricePerTicket * (decimal)exchangeRate,
                    TotalTickets = eventModel.TotalTickets,
                    AvailableTickets = eventModel.AvailableTickets,
                    Image = eventModel.Image

                };
                eventViews.Add(eventViewModel);
            }

            return eventViews;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<EventModel>> GetEvent(int id)
        {
            EventModel? eventModel = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (eventModel != null)
            {
                return eventModel;
            }
            return NotFound("Event with provided ID not found");
        }

        [HttpGet("views/{id}")]
        public async Task<ActionResult<EventViewModel>> GetEventView(int id)
        {
            EventModel? eventModel = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (eventModel != null)
            {
                EventViewModel eventViewModel = new()
                {
                    Id = eventModel.Id,
                    EventName = eventModel.EventName,
                    EventType = eventModel.EventType,
                    EventPlace = eventModel.EventPlace,
                    EventDetails = eventModel.EventDetails,
                    Date = eventModel.Date,
                    PricePerTicket = eventModel.PricePerTicket * (decimal)exchangeRate,
                    TotalTickets = eventModel.TotalTickets,
                    AvailableTickets = eventModel.AvailableTickets,
                    Image = eventModel.Image
                };
                return eventViewModel;
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

        [HttpPut("{eventModelId}")]
        public async Task<ActionResult<List<EventModel>>> UpdateAvailableEventTickets(int? eventModelId, [FromBody]int quantity)
        {
            if (eventModelId != null)
            {
                var foundEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == eventModelId);

                if (foundEvent != null)
                {
                    foundEvent.AvailableTickets = foundEvent.AvailableTickets - quantity;



                    context.ChangeTracker.DetectChanges();

                    //context.Update(foundEvent);

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

        [HttpPost("{exchangerate}")]
        public void SetExchangeRate([FromBody]double exchangerate)
        {
            exchangeRate = exchangerate;
        }

    }
}
