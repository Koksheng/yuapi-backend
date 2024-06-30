using MediatR;
using Microsoft.AspNetCore.Http;
using yuapi.Application.Common.Models;

namespace yuapi.Application.Users.Commands.UpdateUserAvatar
{
    public record UpdateUserAvatarCommand(
        IFormFile file,
        string userState
        ) : IRequest<BaseResponse<int>>;
}
