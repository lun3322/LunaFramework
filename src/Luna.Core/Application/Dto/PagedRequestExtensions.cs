namespace Luna.Application.Dto
{
    public static class PagedRequestExtensions
    {
        public static PagedResponseVmVm<T> ToPagedResponse<T>(this PagedRequestVm @this)
        {
            return new PagedResponseVmVm<T>
            {
                PageIndex = @this.PageIndex,
                PageSize = @this.PageSize
            };
        }
    }
}
