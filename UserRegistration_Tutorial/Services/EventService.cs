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

    public async Task AddNewEvent(EventPostDto eventPostDto)
    {
        var docRef = _db.Collection("calEvent").Document();
        var addNewEvent = _eventsMapper.Map(eventPostDto);
        await docRef.SetAsync(addNewEvent);
    }

    public Task DeleteEventsAsync(string id)
    {
        var eventRef = _db.Collection("calEvent").Document(id);
       return eventRef.DeleteAsync();
    }

     public async Task<bool> UpdateEventAsync(string id, EventUpdateDto model)
    {
        var dbRef = _db.Collection("calEvent").Document(id);
        var docRef = await dbRef.GetSnapshotAsync();


        if (docRef.Exists)
        {
            var updateEvent = _eventsMapper.Map(model);
            await dbRef.UpdateAsync(updateEvent);
            return true;
        }
        return false;
        
    }

    

    public async Task<EventReadDto> GetEventsByIdAsync(string id)
    {
        var dbRef = _db.Collection("calEvent").Document(id);
        var snapshot =  await dbRef.GetSnapshotAsync();
        var eventFromDB = snapshot.ConvertTo<Events>();
        var eventReadDto = _eventsMapper.Map(eventFromDB);
        return eventReadDto;


    }

    
}