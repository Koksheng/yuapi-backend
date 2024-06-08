using MediatR;
using yuapi.Application.Users.Common;

namespace yuapi.Application.Users.Queries.Login
{

    public record UserLoginQuery(string userAccount, string userPassword) : IRequest<UserSafetyResult?>;
}
