using FluentValidation;

namespace yuapi.Application.InterfaceInfos.Queries
{
    public class GetInterfaceInfoByIdQueryValidator : AbstractValidator<GetInterfaceInfoByIdQuery>
    {
        public GetInterfaceInfoByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

        }
    }
}
