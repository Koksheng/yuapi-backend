using yuapi.Application.Common.Models;
using yuapi.Domain.UserInterfaceInfoAggregate;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IUserInterfaceInfoRepository
    {
        Task<int> Add(UserInterfaceInfo userInterfaceInfo);
        Task<UserInterfaceInfo> GetById(int id);
        Task<int> DeleteById(int id);
        Task<int> Update(UserInterfaceInfo userInterfaceInfo);
        Task<List<UserInterfaceInfo>> List(UserInterfaceInfo query);
        Task<PaginatedList<UserInterfaceInfo>> ListByPage(UserInterfaceInfo query, int current, int pageSize, string sortField, string sortOrder);
        Task<UserInterfaceInfo> GetByInterfaceInfoAndUserId(int interfaceInfoId, int userId);
        Task<List<UserInterfaceInfo>> ListTopInvokeInterfaceInfoAsync(int limit);
    }
}
