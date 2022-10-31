using UserRegistration_Tutorial.Authentication;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;

namespace UserRegistration_Tutorial.Services;

public class AuthService : IAuthService
{
    private readonly FirestoreDb _db;
    private readonly UserMapper _userMapper;


    public AuthService(UserMapper userMapper, FirestoreDb db)
    {
        _userMapper = userMapper;
        _db = db;
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
            return null!;
        }
    }

    public async Task RegisterUserAsync(RegisterDto model)
    {
        try
        {
            var addNewUser = _userMapper.Map(model);
            await FirebaseAuth.DefaultInstance.CreateUserAsync(addNewUser);
            var addNewUserToDatabase = _db.Collection("User").Document();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task UpdateUserAsync(UserUpdateInfoDto model)
    {
        var user = FirebaseAuthenticationHandler.User;
        var userFromDataBase = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(user.Email);
        var updatedUser = _userMapper.Map(model, userFromDataBase);
        userFromDataBase = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
    }
}