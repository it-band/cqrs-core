using System.Linq;
using CQRS.Models;

namespace CQRS.Implementation.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPageFilter<T>(this IQueryable<T> query, FilterBase filter)
        {
            var queryOut = query;

            if (filter.Paging.ItemsCount.HasValue)
            {
                var startIndex = filter.Paging.Index * filter.Paging.ItemsCount.Value;

                if (startIndex > 0)
                    queryOut = queryOut.Skip(startIndex);

                return queryOut.Take(filter.Paging.ItemsCount.Value);
            }

            return query;
        }
    }
}
