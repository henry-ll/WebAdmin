using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Framework.Extentions.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public static class AutoInjectionExtention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="iType"></param>
        /// <param name="assemblies"></param>
        public static void AutoInjection(this IServiceCollection services, InjectionType iType, params Assembly[] assemblies)
        {
            var iAllTypes = assemblies[0].GetTypes();//WebAdmin.Infrastructure
            var sAllTypes = assemblies[1].GetTypes();//WebAdmin.Service
            var rAllTypes = assemblies[2].GetTypes();//WebAdmin.Repositories
            switch (iType)
            {
                case InjectionType.moment:
                    #region 瞬时注入
                    /*
                     * 瞬时注入
                     * 每次都构造一个新的实例
                     */
                    foreach (var type in iAllTypes)
                    {
                        if (!type.IsClass && type.IsAbstract && type.Name.Contains("Service") && type.Name.Substring(0, 1) == "I" && !type.Name.Contains("IBaseService`1"))
                        {
                            var name = type.Name.Substring(1);
                            var impl = sAllTypes.FirstOrDefault(w => w.Name== name);
                            if (impl != null)
                                services.AddTransient(type, impl);
                        }
                    }
                    foreach (var type in rAllTypes)
                    {
                        if (type.IsClass && !type.IsAbstract && type.Name.Contains("Repository") && !type.Name.Contains("BaseRepository`1"))
                            services.AddTransient(type);
                    }
                    #endregion
                    break;
                case InjectionType.single:
                    #region 单例注入
                    /*
                     * 单例注入
                     * 任何时候构造的都是同一个实例
                     */
                    foreach (var type in iAllTypes)
                    {
                        if (!type.IsClass && type.IsAbstract && type.Name.Contains("Service") && type.Name.Substring(0, 1) == "I" && !type.Name.Contains("IBaseService`1"))
                        {
                            var name = type.Name.Substring(1);
                            var impl = sAllTypes.FirstOrDefault(w => w.Name == name);
                            if (impl != null)
                                services.AddSingleton(type, impl);
                        }
                    }
                    foreach (var type in rAllTypes)
                    {
                        if (type.IsClass && !type.IsAbstract && type.Name.Contains("Repository") && !type.Name.Contains("BaseRepository`1"))
                            services.AddSingleton(type);
                    }
                    #endregion
                    break;
                case InjectionType.scope:
                    #region 作用域注入
                    /*
                     * 作用域注入
                     * 在同一个作用域中构造的是同一个实例，同一个作用域不同线程也构造的是同一个实例。
                     * 只有在不同的作用域中构造的是不同的实例。(即在同一个请求，都是同一个实例。)
                     */
                    foreach (var type in iAllTypes)
                    {
                        if (!type.IsClass && type.IsAbstract && type.Name.Contains("Service") && type.Name.Substring(0, 1) == "I" && !type.Name.Contains("IBaseService`1"))
                        {
                            var name = type.Name.Substring(1);
                            var impl = sAllTypes.FirstOrDefault(w => w.Name==name);
                            if (impl != null)
                                services.AddScoped(type, impl);
                        }
                    }
                    foreach (var type in rAllTypes)
                    {
                        if (type.IsClass && !type.IsAbstract && type.Name.Contains("Repository") && !type.Name.Contains("BaseRepository`1"))
                            services.AddScoped(type);
                    }
                    #endregion
                    break;
                default:
                    #region 作用域注入
                    /*
                     * 作用域注入
                     * 在同一个作用域中构造的是同一个实例，同一个作用域不同线程也构造的是同一个实例。
                     * 只有在不同的作用域中构造的是不同的实例。(即在同一个请求，都是同一个实例。)
                     */
                    foreach (var type in iAllTypes)
                    {
                        if (!type.IsClass && type.IsAbstract && type.Name.Contains("Service") && type.Name.Substring(0, 1) == "I" && !type.Name.Contains("IBaseService`1"))
                        {
                            var name = type.Name.Substring(1);
                            var impl = sAllTypes.FirstOrDefault(w => w.Name == name);
                            if (impl != null)
                                services.AddScoped(type, impl);
                        }
                    }
                    foreach (var type in rAllTypes)
                    {
                        if (type.IsClass && !type.IsAbstract && type.Name.Contains("Repository") && !type.Name.Contains("BaseRepository`1"))
                            services.AddScoped(type);
                    }
                    #endregion
                    break;
            }
        }
        /// <summary>
        /// 注入类型枚举
        /// </summary>
        public enum InjectionType
        {
            /// <summary>
            /// 瞬时注入
            /// </summary>
            moment = 1,

            /// <summary>
            /// 单例注入
            /// </summary>
            single = 2,

            /// <summary>
            /// 作用域注入
            /// </summary>
            scope = 3,
        }
    }
}
