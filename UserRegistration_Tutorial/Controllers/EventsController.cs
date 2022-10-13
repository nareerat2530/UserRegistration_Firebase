using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddEvent([FromBody] EventPostDto model)
    {
        await _eventService.AddNewEvent(model);


        return StatusCode(200);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getAllEvents = await _eventService.GetAllEventsAsync();
        return Ok(getAllEvents);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        await _eventService.DeleteEventsAsync(id);
        return StatusCode(200);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] EventUpdateDto model)
    {
        if (await _eventService.UpdateEventAsync(id, model)) return StatusCode(200);
        return NotFound("Id does not exist");
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(string id)
    {
        var eventReadDto = await _eventService.GetEventsByIdAsync(id);
      return Ok(eventReadDto);
    }
}