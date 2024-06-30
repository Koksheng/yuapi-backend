using FluentValidation;

namespace yuapi.Application.Users.Commands.RegenerateKey
{
    public class RegenerateKeyCommandValidator : AbstractValidator<RegenerateKeyCommand>
    {
        public RegenerateKeyCommandValidator()
        {
            RuleFor(x => x.userState).NotEmpty();
        }
    }
}
