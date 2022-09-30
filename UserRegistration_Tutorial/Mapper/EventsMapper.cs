using Google.Cloud.Firestore;
using System.Collections.Generic;
using UserRegistration_Tutorial.DTO;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using UserRegistration_Tutorial.Models.Events;

namespace UserRegistration_Tutorial.Mapper
{
    public class EventsMapper
    {
       
        public IEnumerable<EventReadDto> Map(IReadOnlyList<DocumentSnapshot> documents )
        {
           var listOfFieldDictionaries =  documents.Select(d => d.ToDictionary()).ToList();

           var readDtoList =  listOfFieldDictionaries.Select(fieldDictionary => new EventReadDto {
               EventDate = DateTime.ParseExact(fieldDictionary["startDate"].ToString().Replace("Timestamp:", "").Substring(1,10),
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
                EventDate = e.StartDate.ToUniversalTime()


            }).ToList();
        }

        public Events Map(EventPostDto eventPostDto)
        {
            return new()
            {
                Description = eventPostDto.Description,
                StartDate = eventPostDto.EventDate.ToUniversalTime()
            };
        }
    }
}
