using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebAdmin.Framework.Exceptions
{
    /// <summary>
    /// 全局Api异常过滤器
    /// </summary>
    public class GlobalApiExceptionFilter : BaseActionFilterAsync, IAsyncExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public GlobalApiExceptionFilter(IWebHostEnvironment env, ILogger logger)
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
                _logger.Warning(context.Exception, "业务异常："+ exMessage);
                context.Result = Error(exMessage);
            }
            else
            {
                //进行错误日志记录
                _logger.Error(context.Exception, exMessage);
                context.Result = Error("系统繁忙");
            }
            await Task.CompletedTask;
        }
    }
}
