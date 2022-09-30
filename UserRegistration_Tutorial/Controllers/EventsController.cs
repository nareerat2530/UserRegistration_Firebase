using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly FirestoreDb _db;
        public EventsController(FirestoreDb db)
        {
            _db = db;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEvent([FromBody] EventsViewModel model)
        {


            DocumentReference docRef = _db.Collection("calEvent").Document();
            Events events = new Events
            {

                Description = model.Description,
                startDate = model.startDate.ToUniversalTime(),
                
            };
            await docRef.SetAsync(events);

            return Ok(events);

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            CollectionReference collection = _db.Collection("calEvent");
            QuerySnapshot snapshot = await collection.GetSnapshotAsync();
            var getEvents = snapshot.Documents.Select(x => x.ConvertTo<Events>()).ToList();
            return Ok(getEvents);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            DocumentReference eventRef = _db.Collection("calEvent").Document(id);
            await eventRef.DeleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] EventsViewModel model)
        {
            DocumentReference docRef = _db.Collection("calEvent").Document(id);

            Events updateEvents = new Events
            {

                Description = model.Description,
                startDate = model.startDate.ToUniversalTime()
              
            };

            await docRef.SetAsync(updateEvents);
            return Ok();

        }
    }
}

