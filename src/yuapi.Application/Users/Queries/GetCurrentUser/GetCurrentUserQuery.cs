using MediatR;
using yuapi.Application.Users.Common;

namespace yuapi.Application.Users.Queries.GetCurrentUser
{
    public record GetCurrentUserQuery(string userState) : IRequest<UserSafetyResult>;
}
