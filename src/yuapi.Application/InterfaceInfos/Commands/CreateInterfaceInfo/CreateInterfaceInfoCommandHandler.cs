using AutoMapper;
using MediatR;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;
using yuapi.Domain.InterfaceInfoAggregate;

namespace yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo
{
    public class CreateInterfaceInfoCommandHandler :
        IRequestHandler<CreateInterfaceInfoCommand, BaseResponse<int>>
    {
        private readonly IInterfaceInfoRepository _interfaceInfoRepository;
        private readonly IMapper _mapper;

        public CreateInterfaceInfoCommandHandler(IInterfaceInfoRepository interfaceInfoRepository, IMapper mapper)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> Handle(CreateInterfaceInfoCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Map Command to InterfaceInfo
            InterfaceInfo interfaceInfo = _mapper.Map<InterfaceInfo>(command);

            // Persist InterfaceInfo
            var result =  await _interfaceInfoRepository.Add(interfaceInfo);

            // Return InterfaceInfo ID
            if (result == 1)
            {
                //return ResultUtils.success(data: interfaceInfo.Id);
                return ResultUtils.success(data: interfaceInfo.Id.Value);
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }
        }
    }
}
