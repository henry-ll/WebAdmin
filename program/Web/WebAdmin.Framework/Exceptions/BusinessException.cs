using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace WebAdmin.Framework.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessException : System.Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessException() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public BusinessException(string message) : base(message) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="errorCode">错误代码</param>
        public BusinessException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
