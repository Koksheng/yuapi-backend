using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.Common.Models;
using yuapi.Application.InterfaceInfos.Common;
using yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfos;
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
            InterfaceInfo interfaceInfo = _mapper.Map<InterfaceInfo>(query);

            var paginatedResult = await _interfaceInfoRepository.ListByPage(
                interfaceInfo,
                query.Current,
                query.PageSize,
                query.SortField,
                query.SortOrder);

            var result = _mapper.Map<List<InterfaceInfoSafetyResult>>(paginatedResult.Items);

            return new PaginatedList<InterfaceInfoSafetyResult>(result, paginatedResult.TotalCount, query.Current, query.PageSize);
        }
    }
}
