using FluentValidation;

namespace yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfos
{
    public class ListInterfaceInfosQueryValidator : AbstractValidator<ListInterfaceInfosQuery>
    {
        public ListInterfaceInfosQueryValidator()
        {
            //RuleFor(x => x.id).NotEmpty();

        }
    }
}
