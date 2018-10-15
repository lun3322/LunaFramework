namespace Luna.Application.Dto
{
    public static class PagedRequestExtensions
    {
        public static PagedResponseVm<T> ToPagedResponse<T>(this PagedRequestVm @this)
        {
            return new PagedResponseVm<T>
            {
                PageIndex = @this.PageIndex,
                PageSize = @this.PageSize
            };
        }
    }
}
