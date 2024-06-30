using MediatR;
using yuapi.Application.Users.Common;

namespace yuapi.Application.Users.Commands.RegenerateKey
{
    public record RegenerateKeyCommand(
        string userState
        ) : IRequest<UserSafetyResult>;
}
