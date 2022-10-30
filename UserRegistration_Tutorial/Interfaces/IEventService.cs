using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventReadDto>> GetAllEventsAsync();
    Task AddNewEvent(EventPostDto eventPostDto);
    Task DeleteEventsAsync(string id);
    Task<bool> UpdateEventAsync(string id, EventUpdateDto model);
    Task<EventReadDto> GetEventsByIdAsync(string id);
   
}