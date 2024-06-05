using yuapi.Application.Users.Common;

namespace yuapi.Application.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        Task<UserSafetyResult> GetCurrentUserAsync(string userState);
        Task<bool> IsAdminAsync(UserSafetyResult safetyUser);
    }
}
