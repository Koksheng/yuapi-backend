using FluentValidation;

namespace yuapi.Application.Users.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
    {
        public GetCurrentUserQueryValidator()
        {
            RuleFor(x => x.userState).NotEmpty();
                
        }
    }
}
