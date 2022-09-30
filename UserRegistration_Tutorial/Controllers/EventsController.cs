using Google.Cloud.Firestore;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.DTO;
using UserRegistration_Tutorial.Mapper;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Controllers
{
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


            DocumentReference docRef = _db.Collection("calEvent").Document();
            var addNewEvent = _eventsMapper.Map(model);
            await docRef.SetAsync(addNewEvent);

            return StatusCode(200);

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            CollectionReference collection = _db.Collection("calEvent");
            QuerySnapshot snapshot = await collection.GetSnapshotAsync();

            //var eventReadDtos = _eventsMapper.Map(snapshot);
            var eventsList = snapshot.Documents.Select(x => x.ConvertTo<Events>()).ToList();
            var EventReadDTOList = _eventsMapper.Map(eventsList);
            return Ok(EventReadDTOList);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            DocumentReference cityRef = _db.Collection("calEvent").Document(id);
            await cityRef.DeleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] EventsViewModel model)
        {
            DocumentReference docRef = _db.Collection("calEvent").Document(id);

            Events updateEvents = new Events
            {
                Id=id,

                Description = model.Description,
                StartDate = model.StartDate.ToUniversalTime(),
                
            };

            await docRef.SetAsync(updateEvents);
            return Ok();

        }
    }
}

