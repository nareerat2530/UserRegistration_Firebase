namespace UserRegistration_Tutorial.Models.Events
{
    public class Events
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public string UserId { get; set; } = string.Empty;

        public DateTime EndDate { get; set; }

    }
}
