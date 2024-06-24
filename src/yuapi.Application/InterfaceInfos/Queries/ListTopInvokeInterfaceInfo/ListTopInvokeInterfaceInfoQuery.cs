using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.InterfaceInfos.Common;

namespace yuapi.Application.InterfaceInfos.Queries.ListTopInvokeInterfaceInfo
{
    public class ListTopInvokeInterfaceInfoQuery : IRequest<List<InterfaceInfoWithTotalNumResult>>
    {
        public int Limit { get; }

        public ListTopInvokeInterfaceInfoQuery(int limit)
        {
            Limit = limit;
        }
    }
}
