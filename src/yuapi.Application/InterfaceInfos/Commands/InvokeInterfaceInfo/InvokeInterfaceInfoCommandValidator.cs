using FluentValidation;

namespace yuapi.Application.InterfaceInfos.Commands.InvokeInterfaceInfo
{

    public class InvokeInterfaceInfoCommandValidator : AbstractValidator<InvokeInterfaceInfoCommand>
    {
        public InvokeInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();

        }
    }
}
