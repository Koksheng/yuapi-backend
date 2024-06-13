using FluentValidation;

namespace yuapi.Application.UserInterfaceInfos.Commands.UpdateUserInterfaceInfo
{
    public class UpdateUserInterfaceInfoCommandValidator : AbstractValidator<UpdateUserInterfaceInfoCommand>
    {
        public UpdateUserInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();

        }
    }
}
