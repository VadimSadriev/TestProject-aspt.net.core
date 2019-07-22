using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TestProject.Core
{
    public class Filter
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static IQueryable<T> FilterItems<T>(IQueryable<T> items, FilterOptions filterOptions = null)
        {
            return Filtrator.Filter(items, filterOptions.Options);
        }

        private static IQueryable<T> Search<T>(IQueryable<T> query, string propertyName, string searchTerm)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var source = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);

            var body = Expression.Call(source, "Contains", Type.EmptyTypes, Expression.Constant(searchTerm, typeof(string)));

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

            return query.Where(lambda);
        }
    }
}
