using System.ComponentModel;
using UserRegistration_Tutorial.DTO.Events;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Mapper;

public class EventsMapper
{
    public IEnumerable<EventReadDto> Map(IEnumerable<Events> eventsList)
    {
        return eventsList.Select(e => new EventReadDto
        {
            Id = e.Id,
            Description = e.Description,
            EventDate = e.EventDate.ToUniversalTime()
        }).ToList();
    }


    public Events Map(EventPostDto eventPostDto)
    {
        return new Events
        {
            Description = eventPostDto.Description,
            EventDate = eventPostDto.EventDate.ToUniversalTime()
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