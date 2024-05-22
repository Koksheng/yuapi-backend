using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using yuapi.Application.Services.InterfaceInfos;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Domain.Common;
using yuapi.Domain.Entities;
using yuapi.Domain.Exception;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterfaceInfoController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IInterfaceInfoService _interfaceInfoService;

        public InterfaceInfoController(IMapper mapper, IInterfaceInfoService interfaceInfoService)
        {
            _mapper = mapper;
            _interfaceInfoService = interfaceInfoService;
        }

        [HttpPost]
        public async Task<BaseResponse<int>> addInterfaceInfo(InterfaceInfoAddRequest interfaceInfoAddRequest)
        {
            if (interfaceInfoAddRequest == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            InterfaceInfo interfaceInfo = _mapper.Map<InterfaceInfo>(interfaceInfoAddRequest);

            // 校验, will throw Business Exception if fail validate
            _interfaceInfoService.ValidateInterfaceInfo(interfaceInfo);

            interfaceInfo.userId = "123456";

            

            return await _interfaceInfoService.CreateInterfaceInfo(interfaceInfo);
        }
    }
}
