using System;
using System.Linq;
using System.Linq.Expressions;
using AlgoFit.Utils.Pagination.Interfaces;
using AlgoFit.Utils.Pagination.Internal;
using Outland.Utils.Pagination;

namespace AlgoFit.Utils.Pagination
{
    public static class QueryablePagination
    {
        /// <summary>
        /// Create Pagination Result from IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The sequence where the elements are from</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageSize">Number of items to take per page</param>
        /// <returns>An IPaginationResult than contains a selected number of elements from the source and the total number of pages.</returns>
        public static IPaginationResult<T> ToPagination<T>(this IQueryable<T> query, int page, int pageSize)
        {
            IPaginationResult<T> paginationResult = new PaginationResult<T>()
            {
                PagesNumber = query.GetPagesNumber(pageSize),
                Pagination = query.SetPageData(page, pageSize).ToList()
            };

            return paginationResult;
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The sequence to return elements from</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageSize">Number of items to take per page</param>
        /// <returns>
        /// An IQueryable that contains a selected number of elements from the source.
        /// </returns>
        public static IQueryable<T> SetPageData<T>(this IQueryable<T> query, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return query.Skip(skip).Take(pageSize);
        }

        /// <summary>
        /// Return the total number of pages for the selected Page Size
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The sequence where the elements are from</param>
        /// <param name="pageSize">Number of items to take per page</param>
        /// <returns>The Number of Pages</returns>
        public static int GetPagesNumber<T>(this IQueryable<T> query, int pageSize)
        {
            int count = query.Count();
            int pagesNumber = (count + pageSize - 1) / pageSize;
            return pagesNumber;
        }

        public static IQueryable<T> SetOrderBy<T, D>(this IQueryable<T> query, OrderByMap<D, T> dictionaryMap, D? orderBy, OrderType? orderType = OrderType.Asc) where D : struct, Enum
        {
            if (orderBy == null)
            {
                return query;
            }
            if (orderType == null)
            {
                orderType = OrderType.Asc;
            }
            return query.SetOrderBy(dictionaryMap, (D)orderBy, (OrderType)orderType);
        }

        public static IQueryable<T> SetOrderBy<T, D>(this IQueryable<T> query, OrderByMap<D, T> dictionaryMap, D orderBy, OrderType orderType) where D : Enum
        {
            if (dictionaryMap.TryGetValue(orderBy, out Expression<Func<T, object>> value))
            {
                query = query.SetOrderBy(value, orderType);
            }
            return query;
        }

        private static IQueryable<T> SetOrderBy<T>(this IQueryable<T> query, Expression<Func<T, object>> expression, OrderType orderType)
        {
            bool isAscending = orderType != OrderType.Desc;
            if (isAscending)
            {
                query = query.OrderBy(expression);
            }
            else
            {
                query = query.OrderByDescending(expression);
            }
            return query;
        }
    }
}
