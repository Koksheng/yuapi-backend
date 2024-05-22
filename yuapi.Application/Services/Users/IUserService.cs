using yuapi.Contracts.User;
using yuapi.Domain.Common;
using yuapi.Domain.Entities;

namespace yuapi.Application.Services.Users
{
    public interface IUserService
    {
        Task<BaseResponse<int>> UserRegister(UserRegisterRequest request);
        Task<UserSafetyResponse?> UserLogin(string userAccount, string userPassword);
        Task<UserSafetyResponse> GetCurrentUser(string userState);
        //Task<List<UserSafetyResponse>> SearchUserList(SearchUserRequest user);
    }
}
