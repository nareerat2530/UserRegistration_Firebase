using FirebaseAdmin.Auth;
using UserRegistration_Tutorial.Entities;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Models;

namespace UserRegistration_Tutorial.Services
{
    public class UserService : IUserService
    {
        public async Task GetAllUsersAsync()
        {
            var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
            var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
            while (await responses.MoveNextAsync())
            {
                ExportedUserRecords response = responses.Current;
                foreach (ExportedUserRecord user in response.Users)
                {
                    Console.WriteLine($"User: {user.Uid}");
                }
            }

            // Iterate through all users. This will still retrieve users in batches,
            // buffering no more than 1000 users in memory at a time.
            var enumerator = FirebaseAuth.DefaultInstance.ListUsersAsync(null).GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                ExportedUserRecord user = enumerator.Current;
                Console.WriteLine($"User: {user.Uid}");
            }
            
            
           

        }

        public async void Delete(string uid)
        {
            
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
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
            
        }
        //public LoginResponse Authenticate(LoginRequest model)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName || u.PasswordHash == model.Password);
        //    if (user == null)
        //    {
        //        throw new AppException("Username or password is incorrect");


        //    }
        //    var response = new LoginResponse();
        //    {
        //        response.LastName = user.LastName;
        //        response.FirstName = user.FirstName;
        //        response.UserName = user.UserName;

        //    }
        //    return response;




        //}
    }


}

