using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.Models
{
    public class UpdateRequest
    {
        [Required]
        public string uid { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;   
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
