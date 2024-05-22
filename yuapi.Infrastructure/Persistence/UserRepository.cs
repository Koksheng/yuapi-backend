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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByUserAccount(string userAccount)
        {
            var user = await _context.Users
                             .Where(u => !u.isDelete && u.userAccount == userAccount)
                             .FirstOrDefaultAsync();
            return user;
        }

        public async Task<int> CreateUser(User user)
        {
            // Check if user already exists
            var newUser = await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}
