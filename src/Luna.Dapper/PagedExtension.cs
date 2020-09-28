using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using Luna.Application.Dto;

namespace Luna.Dapper
{
    /// <summary>
    ///     PagedExtension
    /// </summary>
    public static class PagedExtension
    {
        private static readonly Regex REPLACE_SELECT_SQL_TO_COUNT_SQL = new Regex("SELECT.+?FROM", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        ///     分页方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="selectSql"></param>
        /// <param name="conditionSql"></param>
        /// <param name="sortField"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static (List<T> data, int count) GetPagedList<T>(this IDbConnection connection, string selectSql, string conditionSql, string sortField,
            object param, IDbTransaction transaction = null)
        {
            if (!(param is RequestPaged))
            {
                throw new ArgumentException($"{nameof(param)}参数类型错误");
            }

            var sql = $@"
{GetPagedSql(selectSql, conditionSql, sortField)}
{GetCountSql(conditionSql)}
";
            using var reader = connection.QueryMultiple(sql, param, transaction);
            var list = reader.Read<T>().ToList();
            var count = reader.ReadSingle<int>();
            return (list, count);
        }

        /// <summary>
        ///     分页方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="selectSql"></param>
        /// <param name="sortField"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static (List<T> data, int count) GetPagedList<T>(this IDbConnection connection, string selectSql, string sortField,
            object param, IDbTransaction transaction = null)
        {
            if (!(param is RequestPaged))
            {
                throw new ArgumentException($"{nameof(param)}参数类型错误");
            }

            var countSql = REPLACE_SELECT_SQL_TO_COUNT_SQL.Replace(selectSql, "SELECT COUNT(1) FROM");

            var sql = $@"
{GetPagedSql(selectSql, sortField)}
{countSql}
";
            using var reader = connection.QueryMultiple(sql, param, transaction);
            var list = reader.Read<T>().ToList();
            var count = reader.ReadSingle<int>();
            return (list, count);
        }


        /// <summary>
        ///     总数sql
        /// </summary>
        /// <param name="conditionSql"></param>
        /// <returns></returns>
        public static string GetCountSql(string conditionSql)
        {
            return $@"
SELECT COUNT(1)
{conditionSql}";
        }

        /// <summary>
        ///     分页语句
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="conditionSql"></param>
        /// <param name="sortField"></param>
        /// <returns></returns>
        public static string GetPagedSql(string selectSql, string conditionSql, string sortField)
        {
            return $@"
{selectSql}
{conditionSql}
ORDER BY {sortField}
OFFSET (@pageIndex - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;";
        }

        /// <summary>
        ///     分页语句
        /// </summary>
        /// <param name="selectSql"></param>
        /// <param name="sortField"></param>
        /// <returns></returns>
        public static string GetPagedSql(string selectSql, string sortField)
        {
            return $@"
{selectSql}
ORDER BY {sortField}
OFFSET (@pageIndex - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;";
        }
    }
}