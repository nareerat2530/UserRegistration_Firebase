﻿
using FirebaseAdmin.Auth;
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
        public async Task<UserRecord> GetById(string uid)
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


    }
}

