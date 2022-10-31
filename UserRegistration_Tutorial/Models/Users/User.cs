namespace UserRegistration_Tutorial.Models.Users;

[FirestoreData]
public class User
{
    [FirestoreDocumentId] public string Uid { get; set; } = string.Empty;

    [FirestoreProperty] public string UserName { get; set; } = string.Empty;

    [FirestoreProperty] public string Email { get; set; } = string.Empty;

    [FirestoreProperty] public ICollection<Events.Events> Event { get; set; }
}