using AutoMapper;
using MediatR;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Models;
using yuapi.Application.InterfaceInfos.Common;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfoByPage
{
    public class ListInterfaceInfoByPageQueryHandler :
        IRequestHandler<ListInterfaceInfoByPageQuery, PaginatedList<InterfaceInfoSafetyResult>>
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly IMapper _mapper;

        public ListInterfaceInfoByPageQueryHandler(IInterfaceInfoRepository interfaceInfoRepository, IMapper mapper)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<InterfaceInfoSafetyResult>> Handle(ListInterfaceInfoByPageQuery query, CancellationToken cancellationToken)
        {
            //var currentPage = query.Current == 0 ? 1 : query.Current;
            //var pageSize = query.PageSize == 0 ? 10 : query.PageSize;
            // Apply default values if necessary
            query.ApplyDefaults();

            InterfaceInfo interfaceInfo = _mapper.Map<InterfaceInfo>(query);
            interfaceInfo.isDelete = 0;
            //interfaceInfo.status = 1;

            var paginatedResult = await _interfaceInfoRepository.ListByPage(
                interfaceInfo,
                query.Current.Value,
                query.PageSize.Value,
                query.SortField,
                query.SortOrder);

            var result = _mapper.Map<List<InterfaceInfoSafetyResult>>(paginatedResult.Items);

            return new PaginatedList<InterfaceInfoSafetyResult>(result, paginatedResult.TotalCount, query.Current.Value, query.PageSize.Value);
        }
    }
}
