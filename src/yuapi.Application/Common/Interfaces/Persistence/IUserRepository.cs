using yuapi.Domain.UserAggregate;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserAccount(string userAccount);
        Task<int> CreateUser(User user);
        Task<User> GetUser(int id);
        Task<User> GetUserInfoByAccessKey(string accessKey);
    }
}
