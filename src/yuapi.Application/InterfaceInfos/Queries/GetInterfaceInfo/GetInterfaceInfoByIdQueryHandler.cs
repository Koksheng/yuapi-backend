using AutoMapper;
using MediatR;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.InterfaceInfos.Common;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.InterfaceInfos.Queries.GetInterfaceInfo
{
    public class GetInterfaceInfoByIdQueryHandler :
        IRequestHandler<GetInterfaceInfoByIdQuery, InterfaceInfoSafetyResult>
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly IMapper _mapper;

        public GetInterfaceInfoByIdQueryHandler(IInterfaceInfoRepository interfaceInfoRepository, IMapper mapper)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
            _mapper = mapper;
        }

        public async Task<InterfaceInfoSafetyResult> Handle(GetInterfaceInfoByIdQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var interfaceInfo = await _interfaceInfoRepository.GetById(query.Id);
            if (interfaceInfo == null)
            {
                throw new BusinessException(ErrorCode.NOT_FOUND_ERROR, "Interface info not found.");
            }

            var result = _mapper.Map<InterfaceInfoSafetyResult>(interfaceInfo);
            return result;
        }
    }
}
