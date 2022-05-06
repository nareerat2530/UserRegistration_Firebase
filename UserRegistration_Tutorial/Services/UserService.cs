using UserRegistration_Tutorial.Entities;
using UserRegistration_Tutorial.Helpers;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Models;

namespace UserRegistration_Tutorial.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public LoginResponse Authenticate(LoginRequest model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
          return  _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);

        }

        public void Register(RegisterRequest model)
        {
            if (_context.Users.Any(x => x.UserName == model.UserName)) throw new AppException($"Username {model.UserName} is already taken");

            var user = new User
            {
               
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
