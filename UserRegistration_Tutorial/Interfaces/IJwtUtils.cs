using UserRegistration_Tutorial.Entities;

namespace UserRegistration_Tutorial.Interfaces
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public string? ValidateToken(string token);
    }
}
