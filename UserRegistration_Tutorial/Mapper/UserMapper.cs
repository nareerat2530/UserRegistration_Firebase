using System.ComponentModel;
using FirebaseAdmin.Auth;
using UserRegistration_Tutorial.DTO.UserDto;

namespace UserRegistration_Tutorial.Mapper;

public class UserMapper
{
    public UserRecordArgs Map(UserUpdateDto model)
    {
        var userRecordArgs = new UserRecordArgs()
        {
            
            DisplayName = model.UserName,
            Password = model.Password

        };
       return userRecordArgs;
        
       
    }
    

}