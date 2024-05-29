using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Data;
using yuapi.Domain.Entities;

namespace yuapi.Infrastructure.Persistence
{
    public class InterfaceInfoRepository : IInterfaceInfoRepository
    {
        private readonly DataContext _context;

        public InterfaceInfoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Domain.InterfaceInfoAggregate.InterfaceInfo interfaceInfo)
        {
            // how to add Domain.InterfaceInfoAggregate.InterfaceInfo into DB using DBContext or do u have other method?
            var newInterfaceInfo =  await _context.InterfaceInfos.AddAsync(interfaceInfo);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        //public async Task<int> CreateInterfaceInfo(InterfaceInfo interfaceInfo)
        //{
        //    // Check if interfaceInfo already exists
        //    var newInterfaceInfo = await _context.InterfaceInfo.AddAsync(interfaceInfo);
        //    var result = await _context.SaveChangesAsync();
            
        //    return result;
        //}
    }
}
