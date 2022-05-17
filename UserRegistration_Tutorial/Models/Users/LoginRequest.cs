using System.ComponentModel.DataAnnotations;

namespace UserRegistration_Tutorial.Models
{
    public class LoginRequest
    {
        [Required]
        public string email { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = null!;

        public bool returnSecureToken = true;
        }
}
