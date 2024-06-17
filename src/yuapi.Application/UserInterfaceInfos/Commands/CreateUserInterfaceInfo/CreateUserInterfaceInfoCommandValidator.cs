using FluentValidation;

namespace yuapi.Application.UserInterfaceInfos.Commands.CreateUserInterfaceInfo
{
    public class CreateUserInterfaceInfoCommandValidator : AbstractValidator<CreateUserInterfaceInfoCommand>
    {
        public CreateUserInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("UserId cannot be empty.")
                .GreaterThan(0).WithMessage("UserId must be greater than zero.");
            RuleFor(x => x.interfaceInfoId)
                .NotEmpty().WithMessage("InterfaceInfoId cannot be empty.")
                .GreaterThan(0).WithMessage("InterfaceInfoId must be greater than zero.");
            RuleFor(x => x.leftNum)
                .GreaterThanOrEqualTo(0).WithMessage("LeftNum cannot be less than zero.");
        }
    }
}
