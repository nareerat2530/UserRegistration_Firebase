
using UserRegistration_Tutorial.Authentication;
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

    // public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
    // {
    //
    //     // var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
    //     //
    //     //
    //     //
    //     //
    //     //
    //     // return pagedEnumerable
    // }

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

    public async Task UpdateUserAsync( UserUpdateInfoDto model)
    {
        var user = FirebaseAuthenticationHandler.User;
        var userFromDataBase = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(user.Email);
        var updatedUser = _userMapper.Map(model, userFromDataBase);
        userFromDataBase = await FirebaseAuth.DefaultInstance.UpdateUserAsync(updatedUser);
    }
}