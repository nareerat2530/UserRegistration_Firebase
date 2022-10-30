namespace UserRegistration_Tutorial.Models.Events;

[FirestoreData]
public class Events
{
    [FirestoreDocumentId] public string Id { get; set; }= string.Empty;

    [FirestoreProperty] public string Description { get; set; } = string.Empty;

    [FirestoreProperty] public DateTime EventDate { get; set; }
    [FirestoreProperty] public string   UserId { get; set; }
}