﻿namespace Luna.Application.Dto
{
    public static class PagedRequestExtensions
    {
        public static ResponsePagedVm<T> ToPagedResponse<T>(this RequestPaged @this)
        {
            return new ResponsePagedVm<T>
            {
                PageIndex = @this.PageIndex,
                PageSize = @this.PageSize
            };
        }
    }
}