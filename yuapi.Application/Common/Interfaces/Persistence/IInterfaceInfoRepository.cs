using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IInterfaceInfoRepository
    {
        Task<int> Add(InterfaceInfo interfaceInfo);
    }
}
