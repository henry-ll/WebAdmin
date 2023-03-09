using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Framework.Extentions.Exceptions
{
    /// <summary>
    /// Cors跨域
    /// </summary>
    public static class ApiCorsExtention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiCors(this IServiceCollection services, WebApplicationBuilder builder)
        {
            //必须appsettings.json中配置
            string corsUrls = builder.Configuration["CorsUrls"];
            if (string.IsNullOrWhiteSpace(corsUrls))
                throw new System.Exception("请配置跨请求的前端Url");
            //添加cors 服务 配置跨域处理
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(corsUrls.Split(","))
                        //添加预检请求过期时间
                         .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                        //如果不需要跨域请注释掉.AllowCredentials()或者增加跨域策略
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApiCors(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
