using Google.Cloud.Firestore;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Services;

public class EventService : IEvent

{
    private readonly FirestoreDb _db;

    public EventService(FirestoreDb db)
    {
        _db = db;
    }

    public Task AddNewEvent()
    {
        throw new NotImplementedException();
    }


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