using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Services;

public class EventService : IEventService

{
    private readonly FirestoreDb _db;
    private readonly EventsMapper _eventsMapper;

    public EventService(FirestoreDb db, EventsMapper eventsMapper)
    {
        _db = db;
        _eventsMapper = eventsMapper;
    }


    public async Task<IEnumerable<EventReadDto>> GetAllEventsAsync()
    {
        var collection = _db.Collection("calEvent");
        var snapshot = await collection.GetSnapshotAsync();
    
        var eventsList = snapshot.Documents.Select(x => x.ConvertTo<Events>()).ToList();
        var eventReadDtoList = _eventsMapper.Map(eventsList);
        return eventReadDtoList;
    }

    public Task AddNewEvent()
    {
        throw new NotImplementedException();
    }

    public Task DeleteEventsAsync(string id)
    {
        var eventRef = _db.Collection("calEvent").Document(id);
       return eventRef.DeleteAsync();
    }

    public Task UpdateEventsAsync(Events events)
    {
        throw new NotImplementedException();
    }

    public Task<DocumentReference> GetEventsByIdAsync(string id)
    {
        var dbRef = _db.Collection("calEvent").Document(id);
        var snap =  dbRef.GetSnapshotAsync();
        return snap;


    }

    
}