using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = null!;
    }
}
