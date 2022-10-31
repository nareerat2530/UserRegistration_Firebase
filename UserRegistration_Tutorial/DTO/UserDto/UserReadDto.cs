namespace UserRegistration_Tutorial.DTO.UserDto;

public class UserReadDto
{
    public string Uid { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    [FirestoreDocumentId] public string EventId { get; set; } = string.Empty;
}