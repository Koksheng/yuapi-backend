using yuapi.Contracts.User;
using yuapi.Domain.Common;

namespace yuapi.Application.Services.Users
{
    public interface IUserService
    {
        Task<BaseResponse<int>> UserRegister(UserRegisterRequest request);
        Task<UserSafetyResponse?> UserLogin(string userAccount, string userPassword);
        Task<UserSafetyResponse> GetCurrentUser(string userState);
        Task<BaseResponse<List<UserSafetyResponse>>?> SearchUserList(SearchUserRequest user);
    }
}
