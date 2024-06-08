using FluentValidation;

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
