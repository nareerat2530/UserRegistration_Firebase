using FirebaseAdmin.Auth;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Models;

namespace UserRegistration_Tutorial.Services;

public class UserService : IUserService
{
    public async Task GetAllUsersAsync()
    {
        var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
        var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
        while (await responses.MoveNextAsync())
        {
            var response = responses.Current;
            foreach (var user in response.Users) Console.WriteLine($"User: {user.Uid}");
        }

        var enumerator = FirebaseAuth.DefaultInstance.ListUsersAsync(null).GetAsyncEnumerator();
        while (await enumerator.MoveNextAsync())
        {
            var user = enumerator.Current;
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
        var user = new UserRecordArgs
        {
            Email = model.Email,
            Password = model.Password,
            DisplayName = model.UserName
        };
        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(user);
    }

    public async Task UpdateAsync(string uid, UpdateRequest model)
    {
        var user = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        var updatedUser = new UserRecordArgs
        {
            Password = model.Password,
            DisplayName = model.UserName
        };
        var userRecord = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
    }
}