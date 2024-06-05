using Microsoft.EntityFrameworkCore;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;
using yuapi.Domain.InterfaceInfoAggregate;
using yuapi.Domain.InterfaceInfoAggregate.ValueObjects;
using yuapi.Domain.UserAggregate.ValueObjects;

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

        public async Task<InterfaceInfo> GetById(int id)
        {
            // Create a UserId object from the provided integer ID
            var interfaceInfoId = InterfaceInfoId.Create(id);

            // Query the database for the user with the specified ID
            var interfaceInfo = await _context.InterfaceInfos
                .FirstOrDefaultAsync(i => i.Id == interfaceInfoId);

            return interfaceInfo;
        }

        public async Task<int> DeleteById(int id)
        {
            // Create a UserId object from the provided integer ID
            var interfaceInfoId = InterfaceInfoId.Create(id);

            // Query the database for the user with the specified ID
            var interfaceInfo = await _context.InterfaceInfos
                .FirstOrDefaultAsync(i => i.Id == interfaceInfoId);

            // Check if the interface was found
            if (interfaceInfo == null)
            {
                throw new BusinessException(ErrorCode.NULL_ERROR, "Interface not found");
            }

            // Update the isDelete column and updateTime column
            interfaceInfo.isDelete = 1;
            interfaceInfo.updateTime = DateTime.UtcNow;

            // Save the changes to the database
            var result = await _context.SaveChangesAsync();

            // Return the result of the deleted interface
            return result;
        }
    }
}
