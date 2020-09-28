namespace Luna.Application.Dto
{
    public class RequestPaged
    {
        public RequestPaged()
        {
            PageIndex = 1;
            PageSize = 15;
        }

        public RequestPaged(int pageIndex, int pageSize = 15)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        ///     页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     每页条数
        /// </summary>
        public int PageSize { get; set; }
    }
}