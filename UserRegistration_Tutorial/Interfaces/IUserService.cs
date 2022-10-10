using UserRegistration_Tutorial.Models.Users;

namespace UserRegistration_Tutorial.Interfaces;

public interface IUserService
{
    Task GetAllUsersAsync();


    Task<UserRecord> GetById(string uid);
    Task RegisterAsync(RegisterRequest model);
    void Delete(string uid);
    Task UpdateAsync(string uid, UpdateRequest model);
}