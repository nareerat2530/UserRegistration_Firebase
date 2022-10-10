using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Mapper;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly FirestoreDb _db;
    private readonly EventsMapper _eventsMapper;

    public EventsController(EventsMapper eventsMapper, FirestoreDb db)
    {
        _db = db;

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
        var collection = _db.Collection("calEvent");
        var snapshot = await collection.GetSnapshotAsync();


        var eventsList = snapshot.Documents.Select(x => x.ConvertTo<Events>()).ToList();
        var eventReadDtoList = EventsMapper.Map(eventsList);
        return Ok(eventReadDtoList);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEvent(string id)
    {
        var eventRef = _db.Collection("calEvent").Document(id);
        await eventRef.DeleteAsync();
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