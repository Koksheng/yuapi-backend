using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Infrastructure.Persistence.Repositories
{
    public class InterfaceInfoRepository : IInterfaceInfoRepository
    {
        private readonly DataContext _context;

        public InterfaceInfoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Add(InterfaceInfo interfaceInfo)
        {
            var newInterfaceInfo = await _context.InterfaceInfos.AddAsync(interfaceInfo);
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
