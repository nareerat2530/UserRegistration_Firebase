using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.Models.Users;

public class RegisterRequest
{
    [Required] public string UserName { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}