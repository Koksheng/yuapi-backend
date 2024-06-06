using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IInterfaceInfoRepository
    {
        Task<int> Add(InterfaceInfo interfaceInfo);
        Task<InterfaceInfo> GetById(int id);
        Task<int> DeleteById(int id);
        Task<int> Update(InterfaceInfo interfaceInfo);
    }
}
