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
       

        public async void Delete(string uid)
        {
            
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
            Console.WriteLine("Successfully deleted user.");

        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public async Task< UserRecord> GetById(string uid)
        {

             return await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

        }

        public async Task RegisterAsync(RegisterRequest model)
        {
            UserRecordArgs user = new UserRecordArgs()
            {
                Email = model.Email,
                Password = model.Password,
                DisplayName = model.UserName,
               
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(user);
        }
        public async Task UpdateAsync(string uid, UpdateRequest model)
        {
            var user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

            UserRecordArgs updatedUser = new UserRecordArgs()
            {
                
                Email = model.Email,
                Password = model.Password,
                DisplayName = model.UserName,
              
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
            // See the UserRecord reference doc for the contents of userRecord.
            Console.WriteLine($"Successfully updated user: {userRecord.Uid}");
        }
       
    }
}
