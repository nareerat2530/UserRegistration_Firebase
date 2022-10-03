using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Security.Cryptography;
using UserRegistration_Tutorial.DTO;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using UserRegistration_Tutorial.Models.Events;
using static Google.Rpc.Context.AttributeContext.Types;
using System.ComponentModel;

namespace UserRegistration_Tutorial.Mapper
{
    public class EventsMapper
    {
       
        public IEnumerable<EventReadDto> Map(IReadOnlyList<DocumentSnapshot> documents )
        {
           var listOfFieldDictionaries =  documents.Select(d => d.ToDictionary()).ToList();

           var readDtoList =  listOfFieldDictionaries.Select(fieldDictionary => new EventReadDto {
               EventDate = DateTime.ParseExact(fieldDictionary["eventDate"].ToString().Replace("Timestamp:", "").Substring(0,10),
               "yyyy-MM-dd", null),
                Description = (string)fieldDictionary["Description"]
              
           }).ToList();

           for (var i = 0; i < readDtoList.Count; i++)
           {
               readDtoList[i].Id = documents[i].Id;
           }

           return readDtoList;

        }

        public IEnumerable<EventReadDto> Map(List<Events> eventsList)
        {
            return eventsList.Select(e => new EventReadDto
            {
                Id = e.Id,
                Description = e.Description,
                EventDate = e.EventDate.ToUniversalTime()


            }).ToList();
        }

        //add controller
        public Events Map(EventPostDto eventPostDto)
        {
            return new()
            {
                Description = eventPostDto.Description,
                EventDate = eventPostDto.EventDate.ToUniversalTime()
            };
        }

        //public Events Map(EventUpdateDTO eventUpdate)
        //{
        //    return new Events()
        //    {
        //        Description = eventUpdate.Description,
        //        EventDate = eventUpdate.EventDate.ToUniversalTime()
        //    };
     

        //}

        internal Dictionary<string, object> Map(EventUpdateDTO model)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(model))
                AddPropertyToDictionary<object>(property, model, dictionary);
            return dictionary;
        }
        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }
    }
}
