using System.Security.Claims;
using Firebase.Auth;
using Google.Apis.Util;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;

using FirebaseAuth = FirebaseAdmin.Auth.FirebaseAuth;


namespace UserRegistration_Tutorial.Services;

public class UserService : IUserService
{
    private readonly UserMapper _userMapper;
    public UserService(UserMapper userMapper)
    {
        _userMapper = userMapper;
    }

    public ValueTask<List<ExportedUserRecord>> GetAllUsersAsync()
    {
        // var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
        // var responses = pagedEnumerable.AsRawResponses().GetAsyncEnumerator();
        // while (await responses.MoveNextAsync())
        // {
        //     var response = responses.Current;
        //     foreach (var user in response.Users) Console.WriteLine($"User: {user.Uid}");
        // }
        //
        // var enumerator = FirebaseAuth.DefaultInstance.ListUsersAsync(null).GetAsyncEnumerator();
        // while (await enumerator.MoveNextAsync())
        // {
        //     var user = enumerator.Current;
        //     Console.WriteLine($"User: {user.Uid}");
        // }
        var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null).ToListAsync();
        return pagedEnumerable;
    }

    public async Task DeleteUserAsync(string uid)
    {
        await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid);
    }

    public async Task<UserRecord> GetUserById(string uid)
    {
       
        try
        {
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
            return userRecord;
        }
        catch
        {
            return null;
        }
        
        
    }

    public async Task RegisterUserAsync(RegisterDto model)
    {
       
        var addNewUser =  _userMapper.Map(model);
        await FirebaseAuth.DefaultInstance.CreateUserAsync(addNewUser);
    }

    public async Task UpdateUserAsync(string uid, UserUpdateInfoDto model)
    {
        var email = User.Claims.Where(c => c.Type == "email")
            .Select(c => c.Value).SingleOrDefault();
        
        var userFromDataBase = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(email);
        
        
        var updatedUser = _userMapper.Map(model, userFromDataBase);
        userFromDataBase = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
    }
}