using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Xml;
using WebAdmin.Framework.Providers;

namespace WebAdmin.Framework.Configs
{
    /// <summary>
    /// Config文件操作
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 
        /// </summary>
        public static IOptions<AppSystem>? Options { get; set; }
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string? GetValue(string key)
        {
            if (Options == null)
                Options = ConfigServiceProvider.ConfigProvider?.GetService<IOptions<AppSystem>>();
            return Options?.Value.AppSettings.Where(x => x.Key == key).FirstOrDefault()?.Value.Trim();
        }
    }
}
