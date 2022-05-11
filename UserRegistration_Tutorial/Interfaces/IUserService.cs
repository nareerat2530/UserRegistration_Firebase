using UserRegistration_Tutorial.Entities;
using UserRegistration_Tutorial.Models;


namespace UserRegistration_Tutorial.Interfaces
{
    public interface IUserService
    {
        LoginResponse Authenticate(LoginRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
        void Delete(int id);
        bool Update(UpdateRequest model, int id);



    }
}
