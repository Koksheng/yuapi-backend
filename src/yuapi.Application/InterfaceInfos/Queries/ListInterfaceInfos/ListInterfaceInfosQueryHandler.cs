using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.InterfaceInfos.Common;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;
using yuapi.Domain.InterfaceInfoAggregate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfos
{
    public class ListInterfaceInfosQueryHandler :
        IRequestHandler<ListInterfaceInfosQuery, List<InterfaceInfoSafetyResult>>
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly IMapper _mapper;

        public ListInterfaceInfosQueryHandler(IInterfaceInfoRepository interfaceInfoRepository, IMapper mapper)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<InterfaceInfoSafetyResult>> Handle(ListInterfaceInfosQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            InterfaceInfo interfaceInfo = _mapper.Map<InterfaceInfo>(query);
            // how to map query.id into interfaceInfo also

            var interfaceInfoList = await _interfaceInfoRepository.List(interfaceInfo);

            var result = _mapper.Map<List<InterfaceInfoSafetyResult>>(interfaceInfoList);

            return result;
        }
    }
}
