using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Luna.Exceptions
{
    /// <summary>
    /// 用户友好的异常
    /// </summary>
    public class LunaUserFriendlyException : Exception
    {
        /// <summary>
        ///     错误码
        /// </summary>
        public int Code { get; set; }

        public LunaUserFriendlyException(string message = null, int code = 0)
            : base(message)
        {
            Code = code;
        }
    }
}