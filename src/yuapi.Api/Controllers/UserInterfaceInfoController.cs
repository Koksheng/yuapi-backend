using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Models;
using yuapi.Application.InterfaceInfos.Commands.CreateInterfaceInfo;
using yuapi.Contracts.InterfaceInfo;
using yuapi.Contracts.UserInterfaceInfo;
using yuapi.Domain.Constants;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserInterfaceInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public UserInterfaceInfoController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse<int>> addUserInterfaceInfo(CreateUserInterfaceInfoRequest request)
        {
            if (request == null)
            {
                throw new BusinessException(ErrorCode.PARAMS_ERROR);
            }

            var command = _mapper.Map<CreateInterfaceInfoCommand>(request);
            return await _mediator.Send(command);
        }
    }
}
