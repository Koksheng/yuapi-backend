using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public CreateInterfaceInfoCommandHandler(IInterfaceInfoRepository interfaceInfoRepository)
        {
            _interfaceInfoRepository = interfaceInfoRepository;
        }

        public async Task<BaseResponse<int>> Handle(CreateInterfaceInfoCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Create InterfaceInfo
            var interfaceInfo = InterfaceInfo.Create(
                name: request.name,
                description: request.description,
                url: request.url,
                requestHeader: request.requestHeader,
                responseHeader: request.responseHeader,
                userId: request.userId,
                status: request.status,
                method: request.method
                );

            // Persist InterfaceInfo
            var result =  await _interfaceInfoRepository.Add(interfaceInfo);

            // Return InterfaceInfo or 1
            if (result == 1)
            {
                //return ResultUtils.success(data: interfaceInfo.Id);
                return ResultUtils.success(data: result);
            }
            else
            {
                throw new BusinessException(ErrorCode.OPERATION_ERROR);
            }
        }
    }
}
