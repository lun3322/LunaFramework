using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Pagination
{
    public static class PagedRequestExtensions
    {
        public static PagedResponse<T> ToPagedResponse<T>(this PagedRequest @this)
        {
            return new PagedResponse<T>
            {
                PageIndex = @this.PageIndex,
                PageSize = @this.PageSize
            };
        }
    }
}
