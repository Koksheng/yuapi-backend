using FluentValidation;

namespace yuapi.Application.Users.Queries.GetKey
{
    public class GetKeyQueryValidator : AbstractValidator<GetKeyQuery>
    {
        public GetKeyQueryValidator() 
        {
            RuleFor(x => x.userState).NotEmpty();
        }
    }
}
