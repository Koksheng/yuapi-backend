using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.UserInterfaceInfos.Common;
using yuapi.Domain.UserInterfaceInfoAggregate;

namespace yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfos
{
    public class ListUserInterfaceInfosQueryHandler :
        IRequestHandler<ListUserInterfaceInfosQuery, List<UserInterfaceInfoSafetyResult>>
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;
        private readonly IMapper _mapper;

        public ListUserInterfaceInfosQueryHandler(IUserInterfaceInfoRepository userInterfaceInfoRepository, IMapper mapper)
        {
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<UserInterfaceInfoSafetyResult>> Handle(ListUserInterfaceInfosQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            UserInterfaceInfo userInterfaceInfo = _mapper.Map<UserInterfaceInfo>(query);
            userInterfaceInfo.status = 1;
            // how to map query.id into userInterfaceInfo also

            var userInterfaceInfoList = await _userInterfaceInfoRepository.List(userInterfaceInfo);

            var result = _mapper.Map<List<UserInterfaceInfoSafetyResult>>(userInterfaceInfoList);

            return result;
        }
    }
}
