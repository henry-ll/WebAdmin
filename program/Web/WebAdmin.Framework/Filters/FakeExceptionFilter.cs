using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Serilog;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Framework.Exceptions;
using WebAdmin.Framework.Extentions;
using WebAdmin.Framework.Primitives;

namespace WebAdmin.Framework.Filters
{
    /// <summary>
    /// 拓展DeveloperExceptionPage
    /// </summary>
    public class FakeExceptionFilter : IDeveloperPageExceptionFilter
    {
        private readonly ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public FakeExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorContext"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task HandleExceptionAsync(ErrorContext errorContext, Func<ErrorContext, Task> next)
        {
            var exMessage = errorContext.Exception.Message; //错误信息
            if (errorContext.Exception.GetType() == typeof(BusinessException))
            {
                _logger.Warning(errorContext.Exception, "业务异常：" + exMessage);
                AjaxResult res = new AjaxResult
                {
                    Success = false,
                    ResponseTime = DateTime.Now,
                    Code = 0,
                    Msg = exMessage,
                };
                await errorContext.HttpContext.Response.WriteAsync(res.ToJson());
            }
            else
                await next.Invoke(errorContext);
        }
    }
}
