using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.Models
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string  Password { get; set; } = string.Empty;
    }
}
