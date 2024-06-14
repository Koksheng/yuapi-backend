using FluentValidation;

namespace yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfoByPage
{
    public class ListUserInterfaceInfoByPageQueryValidator : AbstractValidator<ListUserInterfaceInfoByPageQuery>
    {
        public ListUserInterfaceInfoByPageQueryValidator()
        {
            //RuleFor(x => x.id).NotEmpty();

        }
    }
}
