using Microsoft.AspNetCore.Authorization;
using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Interfaces;

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
        var isAnyPropEmpty = model.GetType().GetProperties()
            .Where(p => p.GetValue(model) is string) // selecting only string props
            .Any(p => string.IsNullOrWhiteSpace(p.GetValue(model) as string));
        if (isAnyPropEmpty) return StatusCode(500, "Description cannot be empty");
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
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        await _eventService.DeleteEventsAsync(id);
        return StatusCode(200);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "FirebaseAuthentication")]
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