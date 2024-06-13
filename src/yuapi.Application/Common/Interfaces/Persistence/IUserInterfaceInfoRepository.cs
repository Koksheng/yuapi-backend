using yuapi.Domain.UserInterfaceInfoAggregate;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IUserInterfaceInfoRepository
    {
        Task<int> Add(UserInterfaceInfo userInterfaceInfo);
        Task<UserInterfaceInfo> GetById(int id);
        Task<int> DeleteById(int id);
        Task<int> Update(UserInterfaceInfo userInterfaceInfo);
    }
}
