using yuapi.Application.Users.Common;
using yuapi.Contracts.User;
using yuapi.Domain.Common;

namespace yuapi.Application.Services.Users
{
    public interface IUserService
    {
        //Task<BaseResponse<int>> UserRegister(UserRegisterRequest request);
        //Task<UserSafetyResponse?> UserLogin(string userAccount, string userPassword);
        Task<UserSafetyResult> GetCurrentUser(string userState);
        Task<bool> IsAdmin(UserSafetyResult safetyUser);
        //Task<BaseResponse<List<UserSafetyResponse>>?> SearchUserList(SearchUserRequest user);
    }
}
