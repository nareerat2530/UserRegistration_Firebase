using Google.Cloud.Firestore;

namespace UserRegistration_Tutorial.DTO
{
    public class EventPostDto
    {
       
  
        public string Description { get; set; } = string.Empty;

        
        public DateTime EventDate { get; set; }
    }
}
