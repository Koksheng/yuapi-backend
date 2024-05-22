using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.Entities;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserAccount(string userAccount);
        Task<int> CreateUser(User user);
        Task<User> GetUser(int id);
    }
}
