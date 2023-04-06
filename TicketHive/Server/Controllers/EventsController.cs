using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Server.Repositories;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepo eventRepo;
        private readonly EventDbContext context;

        public EventsController(IEventRepo eventRepo, EventDbContext context)
        {
            this.eventRepo = eventRepo;
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventModel>>> GetEvents()
        {
            return Ok(await eventRepo.GetAllEvents());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<EventModel>> GetEvent(int id)
        {
            EventModel? eventModel = await eventRepo.GetEvent(id);

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

                await eventRepo.AddEvent(eventModel);

                return Ok("Event added!");
            }
            return BadRequest("Something went wrong when adding event");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<EventModel>>> UpdateEvent(EventModel eventModel, int id)
        {
            if (eventModel != null)
            {
                await eventRepo.UpdateEvent(eventModel, id);
                return Ok(eventRepo.GetAllEvents());
            }
            return BadRequest("Something went wrong when updating event");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<EventModel>>> RemoveEvent(int id)
        {
            await eventRepo.RemoveEvent(id);

            return Ok(eventRepo.GetAllEvents());
        }

    }
}
