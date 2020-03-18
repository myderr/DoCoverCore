// ReSharper disable InconsistentNaming
using DoCover.Common;
using DoCover.Enum;

namespace DoCover.Models
{
    public class Response
    {
        /// <summary>
        /// 操作状态码，详细参考返回代码对照表
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 操作消息，详细参考返回代码对照表
        /// </summary>
        public object message { get; set; }

        public object data { get; set; }

        public Response(ErrorCode errorCode)
        {
            code = (int)errorCode;
            message = errorCode.GetDescription();
        }

        public Response()
        {
            code = 200;
            message = "";
        }

        public Response(int Code, string Message)
        {
            this.code = Code;
            this.message = Message;
        }
        public Response(int Code)
        {
            this.code = Code;
            this.message = "";
        }
    }
}