using FluentValidation;

namespace yuapi.Application.InterfaceInfos.Commands.OnlineInterfaceInfo
{

    public class OnlineInterfaceInfoCommandValidator : AbstractValidator<OnlineInterfaceInfoCommand>
    {
        public OnlineInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();

        }
    }
}
