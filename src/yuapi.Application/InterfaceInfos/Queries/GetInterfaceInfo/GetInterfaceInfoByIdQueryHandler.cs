using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Interfaces.Services;
using yuapi.Application.InterfaceInfos.Common;

namespace yuapi.Application.InterfaceInfos.Queries.GetInterfaceInfo
{
    public class GetInterfaceInfoByIdQueryHandler :
        IRequestHandler<GetInterfaceInfoByIdQuery, InterfaceInfoSafetyResult>
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly IMapper _mapper;
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetInterfaceInfoByIdQueryHandler(IInterfaceInfoRepository interfaceInfoRepository, IMapper mapper, IUserInterfaceInfoRepository userInterfaceInfoRepository, ICurrentUserService currentUserService)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
            _mapper = mapper;
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
            _currentUserService = currentUserService;
        }

        public async Task<InterfaceInfoSafetyResult> Handle(GetInterfaceInfoByIdQuery query, CancellationToken cancellationToken)
        {
            int userInterfaceInfoRemainingCount = 0;
            if (query == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var interfaceInfo = await _interfaceInfoRepository.GetById(query.Id);
            if (interfaceInfo == null)
            {
                throw new BusinessException(ErrorCode.NOT_FOUND_ERROR, "Interface info not found.");
            }

            var safetyUser = await _currentUserService.GetCurrentUserAsync(query.UserState);

            var userInterfaceInfo = await _userInterfaceInfoRepository.GetByInterfaceInfoAndUserId(interfaceInfo.Id.Value, safetyUser.Id);
            if (userInterfaceInfo != null)
            {
                userInterfaceInfoRemainingCount = userInterfaceInfo.leftNum;
            }
            var result = _mapper.Map<InterfaceInfoSafetyResult>(interfaceInfo);
            result = result with { userInterfaceInfoRemainingCount = userInterfaceInfoRemainingCount };

            return result;
        }
    }
}
