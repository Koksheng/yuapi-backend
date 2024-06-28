using FluentValidation;

namespace yuapi.Application.Users.Queries.ListUserByPage
{
    public class ListUserByPageQueryValidator : AbstractValidator<ListUserByPageQuery>
    {
        public ListUserByPageQueryValidator()
        {
            //RuleFor(x => x.id).NotEmpty();
        }
    }
}
