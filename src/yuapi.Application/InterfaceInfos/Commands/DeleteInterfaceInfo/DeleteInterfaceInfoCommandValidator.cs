using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;

namespace yuapi.Application.InterfaceInfos.Commands.DeleteInterfaceInfo
{
    public class DeleteInterfaceInfoCommandValidator : AbstractValidator<DeleteInterfaceInfoCommand>
    {
        public DeleteInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();
                
        }
    }
}
