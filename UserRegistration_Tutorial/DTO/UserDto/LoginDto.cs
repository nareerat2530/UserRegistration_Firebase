using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.DTO.UserDto;

public class LoginDto
{
    [Required] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = null!;
}