using FluentValidation;

namespace yuapi.Application.InterfaceInfos.Queries.GetInterfaceInfo
{
    public class GetInterfaceInfoByIdQueryValidator : AbstractValidator<GetInterfaceInfoByIdQuery>
    {
        public GetInterfaceInfoByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

        }
    }
}
