using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Contracts.User;
using yuapi.Domain.Common;

namespace yuapi.Application.Users.Queries.Login
{
    
    public record UserLoginQuery(string userAccount, string userPassword) : IRequest<UserSafetyResponse?>;
}
