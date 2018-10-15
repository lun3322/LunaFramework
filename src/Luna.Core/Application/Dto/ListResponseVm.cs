using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Application.Dto
{
    /// <summary>
    /// 数组返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListResponseVm<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ListResponseVm()
        {
            Message = "ok";
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
}
