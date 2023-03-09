using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IO;
using WebAdmin.Framework.Extentions;

namespace WebAdmin.Framework.Configs
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppSetting
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration? Configuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static JwtSecret? Secret { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public static SwaggerAuthorizedConfig? SwaggerAuthorized { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public static string TokenHeaderName = "Authorization";
        /// <summary>
        /// 
        /// </summary>
        public static string? CurrentPath { get; private set; } = null;
        /// <summary>
        /// JWT有效期(单位：分钟)
        /// </summary>
        public static int JwtExpMinutes { get; private set; } = 120;
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;
            services.Configure<JwtSecret>(configuration.GetSection("Jwt"));
            services.Configure<SwaggerAuthorizedConfig>(configuration.GetSection("SwaggerAuthorized"));
            var provider = services.BuildServiceProvider();
            IWebHostEnvironment environment = provider.GetRequiredService<IWebHostEnvironment>();
            CurrentPath = Path.Combine(environment.ContentRootPath, "").ReplacePath();
            Secret = provider.GetRequiredService<IOptions<JwtSecret>>().Value;
            JwtExpMinutes = Convert.ToInt32(configuration["JwtExpMinutes"] ?? "120");
            SwaggerAuthorized = provider.GetRequiredService<IOptions<SwaggerAuthorizedConfig>>().Value;
        }
        /// <summary>
        /// 多个节点name格式 ：["key:key1"]
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSettingString(string key)
        {
            return Configuration[key];
        }
        /// <summary>
        /// 多个节点,通过.GetSection("key")["key1"]获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IConfigurationSection GetSection(string key)
        {
            return Configuration.GetSection(key);
        }
    }
}
