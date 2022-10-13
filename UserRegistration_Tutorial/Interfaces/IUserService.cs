using UserRegistration_Tutorial.Models.Users;

namespace UserRegistration_Tutorial.Interfaces;

public interface IUserService
{
    // Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
    Task<UserRecord> GetUserById(string uid);
    Task RegisterUserAsync(RegisterDto model);
    Task DeleteUserAsync(string uid);
    Task UpdateUserAsync(UserUpdateInfoDto model);
}