using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.DTO.UserDto;

public class UserUpdateInfoDto
{
    [Required] public string UserName { get; set; } = string.Empty;
}