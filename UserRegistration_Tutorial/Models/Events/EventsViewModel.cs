using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserRegistration_Tutorial.Models.Events
{
    public class EventsViewModel
    {
       
        public string Description { get; set; } = string.Empty;


        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        

    }
}
