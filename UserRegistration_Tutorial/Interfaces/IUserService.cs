using FirebaseAdmin.Auth;
using UserRegistration_Tutorial.Entities;
using UserRegistration_Tutorial.Models;


namespace UserRegistration_Tutorial.Interfaces
{
    public interface IUserService
    {
        //LoginResponse Authenticate(LoginRequest model);
        IEnumerable<User> GetAll();
        Task <UserRecord> GetById(string uid);
        Task RegisterAsync(RegisterRequest model);
        void Delete(string uid);
        Task UpdateAsync(string uid, UpdateRequest model);
        
    }
}
