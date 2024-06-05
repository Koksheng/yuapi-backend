using FluentValidation;

namespace yuapi.Application.InterfaceInfos.Commands.UpdateInterfaceInfo
{
    public class UpdateInterfaceInfoCommandValidator : AbstractValidator<UpdateInterfaceInfoCommand>
    {
        public UpdateInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();

        }
    }
}
