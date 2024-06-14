using FluentValidation;

namespace yuapi.Application.UserInterfaceInfos.Queries.GetUserInterfaceInfoById
{
    public class GetUserInterfaceInfoByIdQueryValidator : AbstractValidator<GetUserInterfaceInfoByIdQuery>
    {
        public GetUserInterfaceInfoByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

        }
    }
}
