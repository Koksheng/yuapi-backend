using FluentValidation;

namespace yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo
{
    public class CreateInterfaceInfoCommandValidator : AbstractValidator<CreateInterfaceInfoCommand>
    {
        public CreateInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .MaximumLength(50).WithMessage("Name too long, max length 50 characters long.");
        }
    }
}

