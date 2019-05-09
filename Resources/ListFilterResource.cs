using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angular_netcore.Resources
{
    public class ListFilterResource
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }

    }
}
