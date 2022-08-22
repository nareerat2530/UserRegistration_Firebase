namespace UserRegistration_Tutorial.Models
{
    public class Events
    {
        public int Id { get; set; }
        public string EventName { get; set; } = String.Empty;
        public DateTime Start { get; set; }
        public string UserId { get; set; } = String.Empty;
        public string MyProperty { get; set; } = String.Empty;
        public DateTime End { get; set; }
        public string Color { get; set; } = String.Empty;
    }
}
