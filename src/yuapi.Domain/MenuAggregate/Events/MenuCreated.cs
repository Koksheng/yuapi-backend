using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.Common.Models;

namespace yuapi.Domain.MenuAggregate.Events
{
    public record MenuCreated(Menu Menu) : IDomainEvent;
}
