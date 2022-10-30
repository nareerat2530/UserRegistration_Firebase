using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;

namespace UserRegistration_Tutorial.Services;

public class UserService : IUserService
{
    private readonly FirestoreDb _db;
    private readonly UserMapper _userMapper;

    public UserService(FirestoreDb db, UserMapper userMapper)
    {
        _db = db;
        _userMapper = userMapper;
    }
    public Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

  

    public async Task AddNewUser(RegisterDto model)
    {
        var docRef = _db.Collection("User").Document();
        var addNewEvent = _userMapper.MapUser(model);
        await docRef.SetAsync(addNewEvent);
    }

    public Task DeleteUserAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUserAsync(string id, UserUpdateInfoDto model)
    {
        throw new NotImplementedException();
    }

    public Task<UserReadDto> GetUserById(string id)
    {
        throw new NotImplementedException();
    }
}