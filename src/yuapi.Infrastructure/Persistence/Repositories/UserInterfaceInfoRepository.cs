using Microsoft.EntityFrameworkCore;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Domain.UserInterfaceInfoAggregate;
using yuapi.Domain.UserInterfaceInfoAggregate.ValueObjects;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Models;
using yuapi.Application.UserInterfaceInfos.Common;

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

        public async Task<List<UserInterfaceInfo>> List(UserInterfaceInfo query)
        {
            var queryable = _context.UserInterfaceInfos.AsQueryable();

            if (query.Id != null)
            {
                queryable = queryable.Where(i => i.Id == query.Id);
            }

            if (query.userId != null && query.userId > 0)
            {
                queryable = queryable.Where(i => i.userId == query.userId);
            }

            if (query.interfaceInfoId != null && query.interfaceInfoId > 0)
            {
                queryable = queryable.Where(i => i.interfaceInfoId == query.interfaceInfoId);
            }

            if (query.totalNum != null && query.totalNum > 0)
            {
                queryable = queryable.Where(i => i.totalNum == query.totalNum);
            }

            if (query.leftNum != null && query.leftNum > 0)
            {
                queryable = queryable.Where(i => i.leftNum == query.leftNum);
            }

            if (query.status != null)
            {
                queryable = queryable.Where(i => i.status == query.status);
            }

            if (query.isDelete != null)
            {
                queryable = queryable.Where(i => i.isDelete == query.isDelete);
            }

            return await queryable.ToListAsync();
        }

        public async Task<PaginatedList<UserInterfaceInfo>> ListByPage(UserInterfaceInfo query, int current, int pageSize, string sortField, string sortOrder)
        {
            var queryable = _context.UserInterfaceInfos.AsQueryable();

            if (query.Id != null)
            {
                queryable = queryable.Where(i => i.Id == query.Id);
            }

            if (query.userId != null && query.userId > 0)
            {
                queryable = queryable.Where(i => i.userId == query.userId);
            }

            if (query.interfaceInfoId != null && query.interfaceInfoId > 0)
            {
                queryable = queryable.Where(i => i.interfaceInfoId == query.interfaceInfoId);
            }

            if (query.totalNum != null && query.totalNum > 0)
            {
                queryable = queryable.Where(i => i.totalNum == query.totalNum);
            }

            if (query.leftNum != null && query.leftNum > 0)
            {
                queryable = queryable.Where(i => i.leftNum == query.leftNum);
            }

            if (query.status != null)
            {
                queryable = queryable.Where(i => i.status == query.status);
            }

            if (query.isDelete != null)
            {
                queryable = queryable.Where(i => i.isDelete == query.isDelete);
            }

            // Continue with other filters...

            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortOrder == "asc")
                {
                    queryable = queryable.OrderBy(e => EF.Property<object>(e, sortField));
                }
                else
                {
                    queryable = queryable.OrderByDescending(e => EF.Property<object>(e, sortField));
                }
            }

            var totalCount = await queryable.CountAsync();
            var items = await queryable.Skip((current - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<UserInterfaceInfo>(items, totalCount, current, pageSize);
        }

        public async Task<UserInterfaceInfo> GetByInterfaceInfoAndUserId(int interfaceInfoId, int userId)
        {
            // Create a UserId object from the provided integer ID

            // Query the database for the user with the specified ID
            var userInterfaceInfo = await _context.UserInterfaceInfos
                .FirstOrDefaultAsync(i => i.interfaceInfoId == interfaceInfoId && i.userId == userId);

            return userInterfaceInfo;
        }

        public async Task<List<UserInterfaceInfoWithTotalNumResult>> ListTopInvokeInterfaceInfoAsync(int limit)
        {
            var userInterfaceInfo = await _context.UserInterfaceInfos
                .GroupBy(ui => ui.interfaceInfoId)
                .Select(group => new UserInterfaceInfoWithTotalNumResult
                {
                    interfaceInfoId = group.Key,
                    totalNum = group.Sum(ui => ui.totalNum)
                })
                .OrderByDescending(ui => ui.totalNum)
                .Take(limit)
                .ToListAsync();

            return userInterfaceInfo;
        }
    }
}
