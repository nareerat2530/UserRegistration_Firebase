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
            var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName || u.PasswordHash == model.Password);
            if (user == null)
            {
                throw new AppException("Username or password is incorrect");
            }
            var response = new LoginResponse();
            {
                response.LastName = user.LastName;
                response.FirstName = user.FirstName;
                response.UserName = user.UserName;

            }
            return response;
        }

        public void Delete(int id)
        {
            var user = GetUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();

        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            //return _context.Users.FirstOrDefault(x => x.Id == id);
            return GetUser(id);

        }

        public void Register(RegisterRequest model)
        {
            if (_context.Users.Any(x => x.UserName == model.UserName))
                throw new AppException($"Username {model.UserName} is already taken");

            var user = new User
            {

                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Update(int id, UpdateRequest model)
        {
            var user = GetUser(id);

            if (user != null && _context.Users.Any(u => u.UserName != model.UserName))
            {
                throw new AppException($"Username {model.UserName} is already taken");
            }
            var toUpdate = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                PasswordHash = model.Password,
            };
            
            _context.Users.Update(user);
            _context.SaveChanges();
            
        }
            
        private User GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x =>x.Id == id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }
    }
}
