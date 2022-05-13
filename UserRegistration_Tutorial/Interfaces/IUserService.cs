using FirebaseAdmin.Auth;
using UserRegistration_Tutorial.Entities;
using UserRegistration_Tutorial.Models;


namespace UserRegistration_Tutorial.Interfaces
{
    public interface IUserService
    {
        Task GetAllUsersAsync();
        //LoginResponse Authenticate(LoginRequest model);

        Task <UserRecord> GetById(string uid);
        Task RegisterAsync(RegisterRequest model);
        void Delete(string uid);
        Task UpdateAsync(string uid, UpdateRequest model);
        
    }
}
