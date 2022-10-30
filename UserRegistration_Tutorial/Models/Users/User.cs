using System.Text.Json.Serialization;

namespace UserRegistration_Tutorial.Models.Users;
using System.Collections.Generic;
using UserRegistration_Tutorial.Models.Events;
[FirestoreData]
public class User
{
    [FirestoreDocumentId]
    public string Uid { get; set; } = string.Empty;

    [FirestoreProperty] public string UserName { get; set; } = string.Empty;
        [FirestoreProperty]
    public string Email { get; set; } = string.Empty;
    [FirestoreProperty]
    public ICollection<Events> Event { get; set; }





}