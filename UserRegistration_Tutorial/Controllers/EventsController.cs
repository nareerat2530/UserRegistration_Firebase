using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
       private readonly FirestoreDb _db ;

        public EventsController(FirestoreDb db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] EventsViewModel model)
        {

           
            DocumentReference docRef = _db.Collection("calEvent").Document("event");
            Events events = new Events
            {
                
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
            };
            await docRef.SetAsync(events);

            return Ok(events);



        }
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            
            Query allCitiesQuery = _db.Collection("CalEvent");
            QuerySnapshot allCitiesQuerySnapshot = await allCitiesQuery.GetSnapshotAsync();
            //foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            //{
            //    Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
            //    Dictionary<string, object> city = documentSnapshot.ToDictionary();
            //    foreach (KeyValuePair<string, object> pair in city)
            //    {
            //        Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            //    }
            //    Console.WriteLine("");
            //}

            return Ok(allCitiesQuery);

        }
    }
}
