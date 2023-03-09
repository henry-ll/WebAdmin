using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Framework.Exceptions;

namespace WebAdmin.Framework.Filters
{
    /// <summary>
    /// 全局Mvc异常过滤器
    /// </summary>
    public class GlobalMvcExceptionFilter : BaseActionFilterAsync, IAsyncExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public GlobalMvcExceptionFilter(IWebHostEnvironment env, ILogger logger)
        {
            _env = env;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var exMessage = context.Exception.Message; //错误信息
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                _logger.Warning(context.Exception, "业务异常：" + exMessage);
                context.Result = Error(exMessage);
            }
            else
            {
                //进行错误日志记录
                _logger.Error(context.Exception, exMessage);
                context.Result = new RedirectResult("/Error/Error500");
            }
            await Task.CompletedTask;
        }
    }
}
