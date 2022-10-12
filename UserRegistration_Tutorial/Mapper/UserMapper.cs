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

    public UserRecordArgs Map(RegisterDto model )
    {
        var userRecordArgs = new UserRecordArgs()
        {
            Email = model.Email,
            Password = model.Password,
            DisplayName = model.UserName
        };
     return userRecordArgs;
    }
}