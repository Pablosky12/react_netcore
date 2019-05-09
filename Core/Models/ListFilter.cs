using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using react_netcore.Extensions;

namespace angular_netcore.Core.Models
{
    public class ListFilter : IQueryObject
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        
    }
}
