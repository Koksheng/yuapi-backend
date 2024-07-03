using FluentValidation;

namespace yuapi.Application.UserInterfaceInfos.Commands.UpdateFreeTrialUserInterfaceInfo
{
    public class UpdateFreeTrialUserInterfaceInfoCommandValidator : AbstractValidator<UpdateFreeTrialUserInterfaceInfoCommand>
    {
        public UpdateFreeTrialUserInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.userId).NotEmpty();
            RuleFor(x => x.interfaceInfoId).NotEmpty();
            RuleFor(x => x.lockNum)
                .GreaterThan(0).WithMessage("LeftNum cannot be less than zero.");

        }
    }
}
