using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface;

public interface IUserService
{
    Task<Response<int>> UpdateUserGrade(Guid userId, byte grade);

    Task<Response<int>> ChangeEmail(Guid userId, string newEmail);

    Task<Response<int>> ChangePassword(Guid id, UserPasswordChangeDto userPasswordChangeDto);

    Task<Response<int>> AddAdministrator(RegisterAdmin userDto);

    Task<Response<List<UserDto>>> GetUsers(UserQueryParameters parameters);
}