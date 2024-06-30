using MediatR;
using yuapi.Application.Users.Common;

namespace yuapi.Application.Users.Queries.GetKey
{
    public record GetKeyQuery(string userState) : IRequest<UserSafetyResult>;
}
