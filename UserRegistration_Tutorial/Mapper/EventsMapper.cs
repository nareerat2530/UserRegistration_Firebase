using System.ComponentModel;
using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Models.Events;
using UserRegistration_Tutorial.Models.Users;

namespace UserRegistration_Tutorial.Mapper;

public class EventsMapper
{
    public IEnumerable<EventReadDto> Map(IEnumerable<Events> eventsList, User user)
    {
        return eventsList.Select(e => new EventReadDto
        {
            Id = e.Id,
            Description = e.Description,
            EventDate = e.EventDate.ToUniversalTime(),
            UserId = user.Uid
        }).ToList();
    }
    public EventReadDto Map(Events e)
    {
        return new EventReadDto
        {
            Id = e.Id,
            Description = e.Description,
            EventDate = e.EventDate.ToUniversalTime()
            
        };
    }


    public Events Map(EventPostDto eventPostDto, User user)
    {
        return new Events
        {
            Description = eventPostDto.Description,
            EventDate = eventPostDto.EventDate.ToUniversalTime(),
            UserId = user.Uid
        };
    }
    public EventReadDto Map(Dictionary<string, object> snapshotDictionary)
    {
        
        return new EventReadDto()
        {
            
            Description = snapshotDictionary.FirstOrDefault(e => e.Key == "Description").Value.ToString(),
            EventDate = DateTime.ParseExact(snapshotDictionary.FirstOrDefault(e => e.Key == "EventDate").Value.ToString()!.Replace("Timestamp:", "").Substring(1,10), 
                "yyyy-MM-dd", null)
        };
           
    }

    public Dictionary<string, object> Map(EventUpdateDto model)
    {
        var dictionary = new Dictionary<string, object>();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(model))
            AddPropertyToDictionary(property, model, dictionary);
        return dictionary;
    }

    private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source,
        Dictionary<string, T> dictionary)
    {
        var value = property.GetValue(source);
        if (value != null && IsOfType<T>(value))
            dictionary.Add(property.Name, (T)value);
    }

    private static bool IsOfType<T>(object value)
    {
        return value is T;
    }
}