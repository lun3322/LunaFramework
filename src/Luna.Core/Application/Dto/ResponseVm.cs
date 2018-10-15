namespace Luna.Application.Dto
{
    public class ResponseVm
    {
        public ResponseVm(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public ResponseVm()
        {
            Message = "ok";
            Code = 0;
        }

        /// <summary>
        ///     错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     错误码
        /// </summary>
        public int Code { get; set; }

        public static ResponseVm Success(string msg = "ok")
        {
            return new ResponseVm(msg, 0);
        }

        public static ResponseVm Failed(string msg = "error", int code = -1)
        {
            return new ResponseVm(msg, code);
        }
    }

    public class ResponseVm<T> : ResponseVm
    {
        public ResponseVm(string message, int code) : base(message, code)
        {
        }

        public ResponseVm()
        {
        }

        public T Info { get; set; }

        public static ResponseVm<T> Success(T vm)
        {
            return new ResponseVm<T> {Info = vm};
        }
    }
}