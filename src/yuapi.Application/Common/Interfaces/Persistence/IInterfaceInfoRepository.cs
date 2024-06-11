using yuapi.Application.Common.Models;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IInterfaceInfoRepository
    {
        Task<int> Add(InterfaceInfo interfaceInfo);
        Task<InterfaceInfo> GetById(int id);
        Task<int> DeleteById(int id);
        Task<int> Update(InterfaceInfo interfaceInfo);
        Task<List<InterfaceInfo>> List(InterfaceInfo query);
        Task<PaginatedList<InterfaceInfo>> ListByPage(InterfaceInfo query, int current, int pageSize, string sortField, string sortOrder);
        Task<int> OnlineInterfaceInfoById(int id);
        Task<int> OfflineInterfaceInfoById(int id);
    }
}
