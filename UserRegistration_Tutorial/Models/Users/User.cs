using System.Text.Json.Serialization;

namespace UserRegistration_Tutorial.Entities;

public class User
{
    public string uid { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    [JsonIgnore] public string PasswordHash { get; set; } = string.Empty;
}