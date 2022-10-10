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
}