using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Framework.Configs;

namespace WebAdmin.Framework.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class SwaggerBasicAuthMiddleware
    {
        private readonly RequestDelegate next;
        private ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public SwaggerBasicAuthMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            this._logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/doc.html")/*&& !this.IsLocalRequest(context)*/)
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];
                    if (IsAuthorized(username, password))
                    {
                        await next.Invoke(context);
                        return;
                    }
                }
                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
                await next.Invoke(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsAuthorized(string username, string password)
        {
            return username.Equals(AppSetting.SwaggerAuthorized.username, StringComparison.InvariantCultureIgnoreCase)&& password.Equals(AppSetting.SwaggerAuthorized.password);
        }

        public bool IsLocalRequest(HttpContext context)
        {
            if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null)
                return true;
            if (context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
                return true;
            if (IPAddress.IsLoopback(context.Connection.RemoteIpAddress))
                return true;
            return false;
        }
    }
    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}
 