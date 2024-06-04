using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.Common;
using yuapi.Domain.Exception;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterfaceInfoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public InterfaceInfoController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse<int>> addInterfaceInfo(InterfaceInfoAddRequest interfaceInfoAddRequest)
        {
            if (interfaceInfoAddRequest == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            //InterfaceInfo interfaceInfo = _mapper.Map<InterfaceInfo>(interfaceInfoAddRequest);
            var command = _mapper.Map<CreateInterfaceInfoCommand>(interfaceInfoAddRequest);
            // Assign the userId
            command = command with { userId = "123456" };
            return await _mediator.Send(command);

            // 校验, will throw Business Exception if fail validate
            //_interfaceInfoService.ValidateInterfaceInfo(interfaceInfo);

            //interfaceInfo.userId = "123456";


            //return await _interfaceInfoService.CreateInterfaceInfo(interfaceInfo);
        }
    }
}
