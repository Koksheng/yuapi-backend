using FluentValidation;

namespace yuapi.Application.UserInterfaceInfos.Commands.UpdateUserInterfaceInfo
{
    public class UpdateUserInterfaceInfoCommandValidator : AbstractValidator<UpdateUserInterfaceInfoCommand>
    {
        public UpdateUserInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();
            RuleFor(x => x.leftNum)
                .GreaterThanOrEqualTo(0).WithMessage("LeftNum cannot be less than zero.");

        }
    }
}
