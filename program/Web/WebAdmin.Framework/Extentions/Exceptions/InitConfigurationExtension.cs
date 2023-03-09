using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar.Sharding.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Framework.Configs;
using WebAdmin.Framework.Util;

namespace WebAdmin.Framework.Extentions.Exceptions
{
    /// <summary>
    /// 初始化配置
    /// </summary>
    public static class InitConfigurationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="currentDirectory"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection InitConfiguration(this IServiceCollection services, string currentDirectory, IConfiguration configuration)
        {
            IConfiguration Configuration = configuration;
            DatabaseConfig.Configuration = Configuration; //设置数据库参数
            RedisConfig.Configuration = Configuration; //Redis 配置
            AppSetting.Init(services, Configuration);
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwt(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
