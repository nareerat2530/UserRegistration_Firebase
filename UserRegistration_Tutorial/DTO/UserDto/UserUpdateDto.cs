using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.DTO.UserDto;

public class UserUpdateDto
{
    [Required] public string UserName { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}