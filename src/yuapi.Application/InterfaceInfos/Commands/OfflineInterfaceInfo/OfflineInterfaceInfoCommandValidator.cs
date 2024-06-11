using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.InterfaceInfos.Commands.OnlineInterfaceInfo;

namespace yuapi.Application.InterfaceInfos.Commands.OfflineInterfaceInfo
{
    public class OfflineInterfaceInfoCommandValidator : AbstractValidator<OfflineInterfaceInfoCommand>
    {
        public OfflineInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();

        }
    }
}
