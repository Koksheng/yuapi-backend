using Microsoft.EntityFrameworkCore;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Domain.UserInterfaceInfoAggregate;
using yuapi.Domain.UserInterfaceInfoAggregate.ValueObjects;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;

namespace yuapi.Infrastructure.Persistence.Repositories
{
    public class UserInterfaceInfoRepository : IUserInterfaceInfoRepository
    {
        private readonly DataContext _context;

        public UserInterfaceInfoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Add(UserInterfaceInfo userInterfaceInfo)
        {
            var newUserInterfaceInfo = await _context.UserInterfaceInfos.AddAsync(userInterfaceInfo);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        public async Task<UserInterfaceInfo> GetById(int id)
        {
            // Create a UserId object from the provided integer ID
            var userInterfaceInfoId = UserInterfaceInfoId.Create(id);

            // Query the database for the user with the specified ID
            var userInterfaceInfo = await _context.UserInterfaceInfos
                .FirstOrDefaultAsync(i => i.Id == userInterfaceInfoId);

            return userInterfaceInfo;
        }
        public async Task<int> DeleteById(int id)
        {
            // Create a UserId object from the provided integer ID
            var userInterfaceInfoId = UserInterfaceInfoId.Create(id);

            // Query the database for the user with the specified ID
            var userInterfaceInfo = await _context.UserInterfaceInfos
                .FirstOrDefaultAsync(i => i.Id == userInterfaceInfoId);

            // Check if the interface was found
            if (userInterfaceInfo == null)
            {
                throw new BusinessException(ErrorCode.NULL_ERROR, "User Interface Info not found");
            }

            // Update the isDelete column and updateTime column
            userInterfaceInfo.isDelete = 1;
            userInterfaceInfo.updateTime = DateTime.Now;

            // Save the changes to the database
            var result = await _context.SaveChangesAsync();

            // Return the result of the deleted interface
            return result;
        }

        public async Task<int> Update(UserInterfaceInfo userInterfaceInfo)
        {
            // Attach the entity to the context and mark it as modified
            _context.UserInterfaceInfos.Update(userInterfaceInfo);
            // Save the changes to the database
            return await _context.SaveChangesAsync();
        }
    }
}
