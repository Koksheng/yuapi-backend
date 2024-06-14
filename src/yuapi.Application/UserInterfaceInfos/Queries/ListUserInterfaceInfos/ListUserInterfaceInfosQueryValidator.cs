using FluentValidation;

namespace yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfos
{
    public class ListUserInterfaceInfosQueryValidator : AbstractValidator<ListUserInterfaceInfosQuery>
    {
        public ListUserInterfaceInfosQueryValidator()
        {
            //RuleFor(x => x.id).NotEmpty();

        }
    }
}
