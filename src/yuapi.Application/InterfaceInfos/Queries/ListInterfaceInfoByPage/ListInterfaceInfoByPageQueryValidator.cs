using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfos;

namespace yuapi.Application.InterfaceInfos.Queries.ListInterfaceInfoByPage
{
    public class ListInterfaceInfoByPageQueryValidator : AbstractValidator<ListInterfaceInfoByPageQuery>
    {
        public ListInterfaceInfoByPageQueryValidator()
        {
            //RuleFor(x => x.id).NotEmpty();

        }
    }
}
