using FirebaseAdmin.Auth;
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
        //public LoginResponse Authenticate(LoginRequest model)
        //{
        //    //var user = _context.Users.FirstOrDefault(u => u.Email == model|| u.PasswordHash == model.Password);
        //    //if (user == null)
        //    //{
        //    //    throw new AppException("Username or password is incorrect");
        //    //}
        //    //var response = new LoginResponse();
        //    //{
        //    //    response.LastName = user.LastName;
        //    //    response.FirstName = user.FirstName;
        //    //    response.UserName = user.UserName;

        //    //}
        //    //return response;
        //}

        public void Delete(string uid)
        {
            //var user = GetUserAsync(uid);
            //_context.Users.Remove(user);
            //_context.SaveChanges();

        }

       
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public Task< UserRecord> GetById(string uid)
        {
            //return _context.Users.FirstOrDefault(x => x.Id == id);
            return GetUserAsync(uid);

        }

        public void Register(RegisterRequest model)
        {
            //if (_context.Users.Any(x => x.UserName == model.UserName))
            //    throw new AppException($"Username {model.UserName} is already taken");

            //var user = new User
            //{

            //    UserName = model.UserName,
            //    FirstName = model.FirstName,
            //    LastName = model.LastName
            //};

            //_context.Users.Add(user);
            //_context.SaveChanges();
        }
        public async Task UpdateAsync(string uid, UpdateRequest model)
        {
            var user = GetUserAsync(uid);

            //if (user != null && _context.Users.Any(u => u.UserName != model.UserName))
            //{
            //    throw new AppException($"Username {model.UserName} is already taken");
            //}
            UserRecordArgs updatedUser = new UserRecordArgs()
            {
                Uid = uid,
                Email = model.Email,
                Password = model.Password,
                DisplayName = model.UserName,
              
                
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
            // See the UserRecord reference doc for the contents of userRecord.
            Console.WriteLine($"Successfully updated user: {userRecord.Uid}");


        }

      

        private async Task<UserRecord> GetUserAsync(string uid)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
            //var user = _context.Users.FirstOrDefault(x =>x.uid == uid);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }
    }
}
