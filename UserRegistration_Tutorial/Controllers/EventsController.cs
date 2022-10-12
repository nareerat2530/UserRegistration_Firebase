using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly FirestoreDb _db;
    private readonly EventsMapper _eventsMapper;
    private readonly IEventService _eventService;

    public EventsController(EventsMapper eventsMapper, FirestoreDb db, IUserService userService, IEventService eventService)
    {
        _db = db;
        _eventService = eventService;
      

        _eventsMapper = eventsMapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddEvent([FromBody] EventPostDto model)
    {
        var docRef = _db.Collection("calEvent").Document();
        var addNewEvent = _eventsMapper.Map(model);
        await docRef.SetAsync(addNewEvent);

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
        var dbRef = _db.Collection("calEvent").Document(id);
        var docRef = await dbRef.GetSnapshotAsync();
        if (docRef.Exists)
        {
            var updateEvent = _eventsMapper.Map(model);

            await dbRef.UpdateAsync(updateEvent);
            return StatusCode(204);
        }

        return NotFound("Id does not exist");
    }
}