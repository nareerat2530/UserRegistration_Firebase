using Google.Cloud.Firestore;

namespace UserRegistration_Tutorial.Models.Events
{
    [FirestoreData]
    public class Events
    {


        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Description { get; set; } = string.Empty;

        [FirestoreProperty]
        public DateTime StartDate { get; set; }

        [FirestoreProperty]
        public DateTime EndDate { get; set; }

    }
}
