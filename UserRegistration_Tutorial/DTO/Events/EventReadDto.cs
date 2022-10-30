namespace UserRegistration_Tutorial.DTO.Events;

public class EventReadDto
{
    public string Id { get; init; }= string.Empty;

    public string? Description { get; init; } = string.Empty;


    public DateTime EventDate { get; set; }

    public string UserId { get; set; }
}