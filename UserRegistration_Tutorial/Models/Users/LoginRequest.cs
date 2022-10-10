using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.Models;

public class LoginRequest
{
    public bool returnSecureToken = true;

    [Required] public string email { get; set; } = string.Empty;

    [Required] public string password { get; set; } = null!;
}