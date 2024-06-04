using Microsoft.EntityFrameworkCore;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Domain.UserAggregate;
using yuapi.Domain.UserAggregate.Events;
using yuapi.Domain.UserAggregate.ValueObjects;

namespace yuapi.Infrastructure.Persistence.Repositories
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
            var result = await _context.SaveChangesAsync(); // suppose can add Domain Event here, but since UserId is null, cant add Domain Event here
            
            // Add domain event after the user is saved and the ID is generated
            user.AddDomainEvent(new UserCreated(user));

            // Save changes again to persist domain event
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<User> GetUser(int id)
        {
            // Create a UserId object from the provided integer ID
            var userId = UserId.Create(id);

            // Query the database for the user with the specified ID
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }
    }
}
