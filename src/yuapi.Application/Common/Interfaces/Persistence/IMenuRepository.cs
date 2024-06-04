using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.MenuAggregate;

namespace yuapi.Application.Common.Interfaces.Persistence
{
    public interface IMenuRepository
    {
        Task<int> Add(Menu menu);
    }
}
