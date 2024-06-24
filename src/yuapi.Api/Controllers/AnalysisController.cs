using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yuapi.Application.Common.Constants;
using yuapi.Application.Common.Exceptions;
using yuapi.Application.Common.Models;
using yuapi.Application.Common.Utils;
using yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfoByPage;
using yuapi.Contracts.InterfaceInfo;

namespace yuapi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public AnalysisController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<BaseResponse<PaginatedList<InterfaceInfoSafetyResponse>>> listTopInvokeInterfaceInfo()
        {

            var query = _mapper.Map<ListInterfaceInfoByPageQuery>();
            //test 
            var result = await _mediator.Send(query);

            var response = _mapper.Map<PaginatedList<InterfaceInfoSafetyResponse>>(result);

            return ResultUtils.success(response);
        }
    }
}
