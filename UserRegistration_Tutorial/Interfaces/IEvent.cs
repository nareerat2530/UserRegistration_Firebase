using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Interfaces;

public interface IEvent
{
    Task<IEnumerable<Events>> GetEventsAsync();
    Task AddNewEvent();
    bool DeleteEventsAsync(Events events);
    bool UpdateEventsAsync(Events events);
    Task<Events> GetEventsByIdAsync(int id);
}