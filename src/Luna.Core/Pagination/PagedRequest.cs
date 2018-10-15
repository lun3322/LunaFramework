using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Pagination
{
    public class PagedRequest
    {
        public PagedRequest()
        {
            PageIndex = 1;
            PageSize = 15;
        }

        public PagedRequest(int pageIndex = 1, int pageSize = 15)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
