using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Framework.IPLocator;

namespace WebAdmin.Framework.Extentions.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IPLocatorExtention
    {
        /// <summary>
        ///   IPLocatorOption 扩展配置方法
        /// </summary>
        /// <param name="services"></param>
        /// <param name="locatorAction"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureIPLocatorOption(this IServiceCollection services, Action<IPLocatorOption>? locatorAction = null)
        {
            if (locatorAction != null)
            {
                services.Configure(locatorAction);
            }
            return services;
        }
    }
}
