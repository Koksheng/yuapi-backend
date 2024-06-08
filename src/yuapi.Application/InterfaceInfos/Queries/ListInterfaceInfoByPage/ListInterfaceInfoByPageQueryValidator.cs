using FluentValidation;

namespace yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfoByPage
{
    public class ListInterfaceInfoByPageQueryValidator : AbstractValidator<ListInterfaceInfoByPageQuery>
    {
        public ListInterfaceInfoByPageQueryValidator()
        {
            //RuleFor(x => x.id).NotEmpty();

        }
    }
}
