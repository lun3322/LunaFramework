using System.Collections.Generic;

namespace Luna.Application.Dto
{
    /// <summary>
    ///     数组返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseListVm<T> : ResponseVm
    {
        /// <summary>
        ///     ctor
        /// </summary>
        public ResponseListVm()
        {
            Message = "ok";
            Code = 0;
        }

        /// <summary>
        ///     内容
        /// </summary>
        public List<T> Infos { get; set; }
    }
}