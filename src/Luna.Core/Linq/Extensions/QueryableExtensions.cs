﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Luna.Linq.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        ///     分页
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keySelector"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static IQueryable<TSource> Paging<TSource, TKey>(this IQueryable<TSource> source,
            int pageIndex,
            int pageSize,
            Expression<Func<TSource, TKey>> keySelector,
            bool desc = true)
        {
            var linq = source;
            linq = desc ? linq.OrderByDescending(keySelector) : linq.OrderBy(keySelector);

            if (pageIndex > 1)
            {
                var skipCount = (pageIndex - 1) * pageSize;
                linq = linq.Skip(skipCount);
            }

            return linq.Take(pageSize);
        }

        /// <summary>
        ///     条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
    }
}