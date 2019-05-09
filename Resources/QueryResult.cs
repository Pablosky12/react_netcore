using System.Collections.Generic;

namespace react_netcore.Core.Models
{
    public class QueryResultResource<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems {get;set;}
    }
}