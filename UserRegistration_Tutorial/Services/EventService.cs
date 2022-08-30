using Google.Cloud.Firestore;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Services
{

    
    public class EventService : IEvent

    {
        readonly FirestoreDb _db = FirestoreDb.Create("firebase - with - dotnet");

        public EventService(FirestoreDb db )
        {
            _db = db;
        }

        public Task AddNewEvent()
        {
            throw new NotImplementedException();
        }

        //        public async Task AddNewEvent(Events events)
        //        {
        //            DocumentReference docRef = _db.Collection("calEvent").Document("alovelace");
        //            Dictionary<string, object> Event = new Dictionary<string, object>
        //            {



        //            }
        //{              

        //};
        //            await docRef.SetAsync(Event);
        //        }

        public bool DeleteEventsAsync(Events events)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Events>> GetEventsAsync()
        {
           throw new NotImplementedException();
        }

        public Task<Events> GetEventsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEventsAsync(Events events)
        {
            throw new NotImplementedException();
        }
    }
}
