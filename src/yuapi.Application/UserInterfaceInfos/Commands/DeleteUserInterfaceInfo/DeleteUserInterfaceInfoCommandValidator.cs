using FluentValidation;

namespace yuapi.Application.UserInterfaceInfos.Commands.DeleteUserInterfaceInfo
{
    public class DeleteUserInterfaceInfoCommandValidator : AbstractValidator<DeleteUserInterfaceInfoCommand>
    {
        public DeleteUserInterfaceInfoCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();

        }
    }
}
