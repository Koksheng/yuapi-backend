using AutoMapper;
using MediatR;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Models;
using yuapi.Application.UserInterfaceInfos.Common;
using yuapi.Domain.UserInterfaceInfoAggregate;

namespace yuapi.Application.UserInterfaceInfos.Queries.ListUserInterfaceInfoByPage
{
    public class ListUserInterfaceInfoByPageQueryHandler :
        IRequestHandler<ListUserInterfaceInfoByPageQuery, PaginatedList<UserInterfaceInfoSafetyResult>>
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;
        private readonly IMapper _mapper;

        public ListUserInterfaceInfoByPageQueryHandler(IUserInterfaceInfoRepository userInterfaceInfoRepository, IMapper mapper)
        {
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserInterfaceInfoSafetyResult>> Handle(ListUserInterfaceInfoByPageQuery query, CancellationToken cancellationToken)
        {
            // Apply default values if necessary
            query.ApplyDefaults();

            UserInterfaceInfo userInterfaceInfo = _mapper.Map<UserInterfaceInfo>(query);
            userInterfaceInfo.isDelete = 0;
            //userInterfaceInfo.status = 1;

            var paginatedResult = await _userInterfaceInfoRepository.ListByPage(
                userInterfaceInfo,
                query.Current.Value,
                query.PageSize.Value,
                query.SortField,
                query.SortOrder);

            var result = _mapper.Map<List<UserInterfaceInfoSafetyResult>>(paginatedResult.Items);

            return new PaginatedList<UserInterfaceInfoSafetyResult>(result, paginatedResult.TotalCount, query.Current.Value, query.PageSize.Value);
        }
    }
}
