using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.UserInterfaceInfos.Common;

namespace yuapi.Application.UserInterfaceInfos.Queries.GetUserInterfaceInfoById
{
    public class GetUserInterfaceInfoByIdQueryHandler :
        IRequestHandler<GetUserInterfaceInfoByIdQuery, UserInterfaceInfoSafetyResult>
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;
        private readonly IMapper _mapper;

        public GetUserInterfaceInfoByIdQueryHandler(IUserInterfaceInfoRepository userInterfaceInfoRepository, IMapper mapper)
        {
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
            _mapper = mapper;
        }

        public async Task<UserInterfaceInfoSafetyResult> Handle(GetUserInterfaceInfoByIdQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var userInterfaceInfo = await _userInterfaceInfoRepository.GetById(query.Id);
            if (userInterfaceInfo == null)
            {
                throw new BusinessException(ErrorCode.NOT_FOUND_ERROR, "User Interface Info not found.");
            }

            var result = _mapper.Map<UserInterfaceInfoSafetyResult>(userInterfaceInfo);
            return result;
        }
    }
}
