using UserRegistration_Tutorial.Models.Users;

namespace UserRegistration_Tutorial.Mapper;

public class UserMapper
{
    public UserRecordArgs Map(UserUpdateInfoDto model, UserRecord userFromDataBase)
    {
        return new UserRecordArgs
        {
            Uid = userFromDataBase.Uid,
            DisplayName = model.UserName
        };
    }

    public UserRecordArgs Map(RegisterDto model)
    {
        var userRecordArgs = new UserRecordArgs
        {
            Email = model.Email,
            Password = model.Password,
            DisplayName = model.UserName
        };
        return userRecordArgs;
    }

    public User MapUser(RegisterDto model)
    {
        var user = new User
        {
            Email = model.Email,
            UserName = model.UserName
        };
        return user;
    }

    public IEnumerable<UserReadDto> Map(List<ExportedUserRecord> exportedUserRecord)
    {
        return exportedUserRecord.Select(user => new UserReadDto
        {
            Uid = user.Uid,
            Email = user.Email,
            UserName = user.DisplayName
        }).ToList();
    }
}