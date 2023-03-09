using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Framework.Configs;
using WebAdmin.Framework.Providers;

namespace WebAdmin.Framework.Extentions.Exceptions
{
    /// <summary>
    /// 使用多静态文件夹访问
    /// </summary>
    public static class ManyStaticFilesExtention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddManyStaticFiles(this IServiceCollection services, WebApplicationBuilder builder, IConfiguration Configuration)
        {
            builder.Services.Configure<VirtualPathConfig>(Configuration);//注入多个静态文件访问目录
            var config = Configuration.Get<VirtualPathConfig>().VirtualPath;
            config.ForEach(f =>
            {
                Directory.CreateDirectory(f.RealPath);
                builder.Services.AddSingleton(new ManyStaticFileProvider(f.RealPath, f.Alias));
            });
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseManyStaticFiles(this IApplicationBuilder app, IConfiguration Configuration)
        {
            //启用wwwroot 默认文件夹访问
            app.UseStaticFiles();
            var config = Configuration.Get<VirtualPathConfig>().VirtualPath;
            config.ForEach(f =>
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(f.RealPath)),
                    RequestPath = f.RequestPath
                });
            });
            return app;
        }
    }
}
