namespace UserRegistration_Tutorial.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
    Task AddNewUser(RegisterDto model);
    Task DeleteUserAsync(string id);
    Task<bool> UpdateUserAsync(string id, UserUpdateInfoDto model);
    Task<UserReadDto> GetUserById(string id);
}