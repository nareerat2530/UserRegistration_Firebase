namespace UserRegistration_Tutorial.DTO.Events;

public class EventReadDto
{
    public string Id { get; set; }

    public string Description { get; set; } = string.Empty;


    public DateTime EventDate { get; set; }
}