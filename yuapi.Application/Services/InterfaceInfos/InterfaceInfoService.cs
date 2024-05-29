//using yuapi.Application.Common.Interfaces.Persistence;
//using yuapi.Domain.Common;
//using yuapi.Domain.Entities;
//using yuapi.Domain.Exception;

//namespace yuapi.Application.Services.InterfaceInfos
//{
//    public class InterfaceInfoService : IInterfaceInfoService
//    {


//        private readonly IInterfaceInfoRepository _interfaceInfoRepository;

//        public InterfaceInfoService(IInterfaceInfoRepository interfaceInfoRepository)
//        {
//            _interfaceInfoRepository = interfaceInfoRepository;
//        }

//        public async Task<BaseResponse<int>> CreateInterfaceInfo(InterfaceInfo interfaceInfo)
//        {
//            var result = await _interfaceInfoRepository.CreateInterfaceInfo(interfaceInfo);
//            if(result == 1)
//            {
//                return ResultUtils.success(interfaceInfo.Id);
//            }
//            else
//            {
//                throw new BusinessException(ErrorCode.OPERATION_ERROR);
//            }
//        }

//        public void ValidateInterfaceInfo(InterfaceInfo interfaceInfo)
//        {
//            if (interfaceInfo == null)
//            {
//                throw new BusinessException(ErrorCode.PARAMS_ERROR);
//            }


//            string name = interfaceInfo.name;
//            //创建时，参数不能为空
//            //if (add)
//            //{
//            //    ThrowUtils.throwIf(StringUtils.isAnyBlank(name), ErrorCode.PARAMS_ERROR);
//            //}
//            // 有参数则校验
//            if (string.IsNullOrWhiteSpace(name))
//            {
//                throw new BusinessException(ErrorCode.PARAMS_ERROR, "接口名称不能为空");
//            }
//            if (name.Length > 50)
//            {
//                throw new BusinessException(ErrorCode.PARAMS_ERROR, "接口名称过长");
//            }
//        }
//    }
//}
