using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Pagination
{
    /// <summary>
    /// 数组返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseList<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ResponseList()
        {
            Message = "Ok";
            Code = 0;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public List<T> Infos { get; set; }
    }

    /// <summary>
    /// 分页返回值
    /// </summary>
    public class PagedResponse<T> : ResponseList<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}
