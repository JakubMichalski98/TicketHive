using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketHive.Server.Repositories;
using TicketHive.Shared.Models;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepo eventRepo;

        public EventsController(IEventRepo eventRepo)
        {
            this.eventRepo = eventRepo;
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

    }
}
