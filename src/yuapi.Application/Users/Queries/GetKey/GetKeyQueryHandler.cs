using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.Users.Common;

namespace yuapi.Application.Users.Queries.GetKey
{
    public class GetKeyQueryHandler :
        IRequestHandler<GetKeyQuery, UserSafetyResult>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetKeyQueryHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<UserSafetyResult> Handle(GetKeyQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }
            string userState = query.userState;

            var safetyUser = await _currentUserService.GetCurrentUserAsync(userState);

            return safetyUser;
        }
    }
}
