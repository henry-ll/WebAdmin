using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using WebAdmin.Framework.Configs;

namespace WebAdmin.Framework.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigServiceProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public static ServiceProvider? ConfigProvider { get; set; }
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Init(IServiceCollection services)
        {
            ConfigProvider = services.BuildServiceProvider();
        }
    }
}
