using MediatR;
using yuapi.Application.Common.Models;

namespace yuapi.Application.Users.Commands.Register
{
    public record UserRegisterCommand(string userAccount, string userPassword, string checkPassword) : IRequest<BaseResponse<int>>;
}
