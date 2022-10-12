using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.DTO.UserDto;

public class RegisterDto
{
    [Required] public string UserName { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}