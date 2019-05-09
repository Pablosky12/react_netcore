using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using angular_netcore.Core.Models;

namespace react_netcore.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> vehicleQuery, IQueryObject filter, Dictionary<string, Expression<Func<T, object>>> map)
        {
            if (String.IsNullOrWhiteSpace(filter.SortBy) || !map.ContainsKey(filter.SortBy))
                return vehicleQuery;
            if (filter.IsSortAscending)
                return vehicleQuery.OrderBy(map[filter.SortBy]);
            else
                return vehicleQuery.OrderByDescending(map[filter.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> vehicleQuery, IQueryObject filter)
        {
            return vehicleQuery.Skip((filter.PageNumber-1) * filter.PageSize).Take(filter.PageSize);
        }
    }
}