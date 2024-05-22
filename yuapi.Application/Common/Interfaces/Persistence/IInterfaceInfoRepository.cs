using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.Entities;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IInterfaceInfoRepository
    {
        Task<int> CreateInterfaceInfo(InterfaceInfo interfaceInfo);
    }
}
