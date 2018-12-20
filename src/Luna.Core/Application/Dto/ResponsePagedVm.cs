namespace Luna.Application.Dto
{
    /// <summary>
    ///     分页返回值
    /// </summary>
    public class ResponsePagedVm<T> : ResponseListVm<T>
    {
        /// <summary>
        ///     页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     总数
        /// </summary>
        public int Total { get; set; }
    }
}