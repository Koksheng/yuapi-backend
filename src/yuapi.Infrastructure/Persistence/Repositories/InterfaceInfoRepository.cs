﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;
using yuapi.Domain.InterfaceInfoAggregate;
using yuapi.Domain.InterfaceInfoAggregate.ValueObjects;

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
            interfaceInfo.updateTime = DateTime.Now;

            // Save the changes to the database
            var result = await _context.SaveChangesAsync();

            // Return the result of the deleted interface
            return result;
        }

        public async Task<int> Update(InterfaceInfo interfaceInfo)
        {
            // Attach the entity to the context and mark it as modified
            _context.InterfaceInfos.Update(interfaceInfo);
            // Save the changes to the database
            return await _context.SaveChangesAsync();
        }

        public async Task<List<InterfaceInfo>> List(InterfaceInfo query)
        {
            var queryable = _context.InterfaceInfos.AsQueryable();

            if (query.Id != null)
            {
                queryable = queryable.Where(i => i.Id == query.Id);
            }

            if (!string.IsNullOrEmpty(query.name))
            {
                queryable = queryable.Where(i => i.name.Contains(query.name));
            }

            if (!string.IsNullOrEmpty(query.description))
            {
                queryable = queryable.Where(i => i.description.Contains(query.description));
            }

            if (!string.IsNullOrEmpty(query.url))
            {
                queryable = queryable.Where(i => i.url.Contains(query.url));
            }

            if (!string.IsNullOrEmpty(query.requestHeader))
            {
                queryable = queryable.Where(i => i.requestHeader.Contains(query.requestHeader));
            }

            if (!string.IsNullOrEmpty(query.responseHeader))
            {
                queryable = queryable.Where(i => i.responseHeader.Contains(query.responseHeader));
            }

            if (query.status != null)
            {
                queryable = queryable.Where(i => i.status == query.status);
            }

            if (!string.IsNullOrEmpty(query.method))
            {
                queryable = queryable.Where(i => i.method.Contains(query.method));
            }

            if (query.userId != null)
            {
                queryable = queryable.Where(i => i.userId == query.userId);
            }

            return await queryable.ToListAsync();
        }
    }
}