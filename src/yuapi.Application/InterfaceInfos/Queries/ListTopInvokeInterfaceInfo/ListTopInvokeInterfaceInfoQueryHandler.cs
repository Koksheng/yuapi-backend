using AutoMapper;
using MediatR;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Application.InterfaceInfos.Common;

namespace yuapi.Application.InterfaceInfos.Queries.ListTopInvokeInterfaceInfo
{
    public class ListTopInvokeInterfaceInfoQueryHandler : IRequestHandler<ListTopInvokeInterfaceInfoQuery, List<InterfaceInfoWithTotalNumResult>>
    {
        private readonly IUserInterfaceInfoRepository _userInterfaceInfoRepository;
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly IMapper _mapper;

        public ListTopInvokeInterfaceInfoQueryHandler(IUserInterfaceInfoRepository userInterfaceInfoRepository, IInterfaceInfoRepository interfaceInfoRepository, IMapper mapper)
        {
            _userInterfaceInfoRepository = userInterfaceInfoRepository;
            _interfaceInfoRepository = interfaceInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<InterfaceInfoWithTotalNumResult>> Handle(ListTopInvokeInterfaceInfoQuery query, CancellationToken cancellationToken)
        {
            var limit = query.Limit;
            var userInterfaceInfoList = await _userInterfaceInfoRepository.ListTopInvokeInterfaceInfoAsync(limit);

            var interfaceInfoIdObjMap = userInterfaceInfoList.GroupBy(u => u.interfaceInfoId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var interfaceIds = interfaceInfoIdObjMap.Keys.ToList();
            var interfaceInfoList = await _interfaceInfoRepository.ListByIdsAsync(interfaceIds);
            
            if (!interfaceInfoList.Any())
            {
                throw new BusinessException(ErrorCode.SYSTEM_ERROR);
            }

            var interfaceInfoWithTotalNumList = interfaceInfoList.Select(interfaceInfo =>
            {
                var totalNum = interfaceInfoIdObjMap[interfaceInfo.Id.Value].First().totalNum;
                return new InterfaceInfoWithTotalNumResult(
                    interfaceInfo.Id.Value,
                    interfaceInfo.name,
                    interfaceInfo.description,
                    interfaceInfo.url,
                    interfaceInfo.requestParams,
                    interfaceInfo.requestHeader,
                    interfaceInfo.responseHeader,
                    interfaceInfo.status,
                    interfaceInfo.method,
                    interfaceInfo.createTime,
                    interfaceInfo.updateTime,
                    totalNum
                );
            }).ToList();

            return interfaceInfoWithTotalNumList;
        }
    }
}
