using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuapi.Contracts.Common
{
    public class PageRequest
    {
        public int? Current { get; set; } = 1; // Default page number
        public int? PageSize { get; set; } = 10; // Default page size
        public string? SortField { get; set; }
        public string? SortOrder { get; set; }
    }
}
