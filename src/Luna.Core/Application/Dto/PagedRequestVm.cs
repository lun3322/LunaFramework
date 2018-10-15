namespace Luna.Application.Dto
{
    public class PagedRequestVm
    {
        public PagedRequestVm()
        {
            PageIndex = 1;
            PageSize = 15;
        }

        public PagedRequestVm(int pageIndex = 1, int pageSize = 15)
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
