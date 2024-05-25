using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.Common;

namespace yuapi.Application.User.Commands.Register
{
    public record UserRegisterCommand(string userAccount, string userPassword, string checkPassword) : IRequest<BaseResponse<int>>;
}
