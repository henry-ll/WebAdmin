using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Net.Http;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAdmin.Framework.Middleware
{
    /// <summary>
    /// 请求参数过滤中间件
    /// </summary>
    public class ParamsMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next"></param>
        public ParamsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            try
            {
                var requestType = context.Request.Method;
                if (requestType.ToUpper() == "GET")
                {
                    var query = context.Request.Query;
                    var dic = new Dictionary<string, StringValues>();
                    if (query != null && query.Count > 0)
                    {
                        foreach (var item in query)
                            dic.Add(item.Key, new StringValues(ReplaceParam(item.Value.ToString())));
                        context.Request.Query = new QueryCollection(dic);
                    }
                }
                else
                {
                    var request = context.Request;
                    if (!string.IsNullOrWhiteSpace(request.QueryString.ToString()))
                    {
                        var query = context.Request.Query;
                        var dic = new Dictionary<string, StringValues>();
                        if (query != null && query.Count > 0)
                        {
                            foreach (var item in query)
                                dic.Add(item.Key, new StringValues(ReplaceParam(item.Value.ToString())));
                            context.Request.Query = new QueryCollection(dic);
                        }
                    }
                    else
                    {
                        var from = context.Request.Form;
                        var dic = new Dictionary<string, StringValues>();
                        if (from != null && from.Count > 0)
                        {
                            foreach (var item in from)
                            {
                                if (item.Key.Contains("[]"))
                                    dic.Add(item.Key, new StringValues(ReplaceParam(item.Value.ToArray())));
                                else
                                    dic.Add(item.Key, new StringValues(ReplaceParam(item.Value.ToString())));
                            }
                            context.Request.Form = new FormCollection(dic);
                        }
                    }
                }
                return _next(context);
            }
            catch (Exception)
            {
                return _next(context);
            }
        }

        /// <summary>
        /// 替换参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ReplaceParam(string param)
        {
            if (!string.IsNullOrWhiteSpace(param))
                param = param.Trim();

            //不安全的字符串
            //param = param.Replace("'", "");
            //param = param.Replace("\"", "");
            //param = param.Replace("<", "");
            //param = param.Replace(">", "");
            //param = param.Replace("*", "");
            //param = param.Replace("?", "");
            //param = param.Replace(";", "");
            //param = param.Replace("*/", "");
            //param = param.Replace("#", "");
            //param = param.Replace("--", "");
            param = param.Replace("\r\n", "");
            param = param.Replace("\r", "");
            param = param.Replace("\n", "");
            //删除与数据库相关的词
            param = Regex.Replace(param, "select ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "insert ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "update ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "delete ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "from ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "count'' ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "drop table ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "truncate ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "asc ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "mid ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "char ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "xp_cmdshell ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "exec master ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "net localgroup administrators ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "and ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "net user ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "or ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "net ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "set ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "drop ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "script ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "call ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "having 1 = 1-- ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "; insert ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "; select ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "; insert ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "; update ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "; delete ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "; drop table ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "; truncate ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "--insert ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "--select ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "--insert ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "--update ", "", RegexOptions.IgnoreCase);
            param = Regex.Replace(param, "--delete ", "", RegexOptions.IgnoreCase);
            return param;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string[] ReplaceParam(string[] param)
        {
            for (int i = 0; i < param.Length; i++)
                param[i] = ReplaceParam(param[i]);
            return param;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class ParamsMiddlewareExtensions
    {
        ///
        public static IApplicationBuilder UseParamsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ParamsMiddleware>();
        }
    }
}
