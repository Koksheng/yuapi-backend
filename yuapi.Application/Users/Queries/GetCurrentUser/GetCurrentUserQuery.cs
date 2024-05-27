using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.Users.Common;
using yuapi.Contracts.User;

namespace yuapi.Application.Users.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery(string userState) : IRequest<UserSafetyResult>;
}
