using Google.Cloud.Firestore;

namespace UserRegistration_Tutorial.DTO
{
    public class EventReadDto
    {
        public string Id { get; set; }
  
        public string Description { get; set; } = string.Empty;

        
        public DateTime EventDate { get; set; }
    }
}
