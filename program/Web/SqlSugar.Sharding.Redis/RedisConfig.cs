using Microsoft.Extensions.Configuration;
namespace SqlSugar.Sharding.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration? Configuration { get; set; }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static RedisConfigInfo GetRedisConfig()
        {
            return GetConfig();
        }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="sectionName">xml节点名称</param>
        /// <returns></returns>
        public static RedisConfigInfo GetConfig()
        {
            return new RedisConfigInfo()
            {
                WriteServerList = Configuration?["RedisConfig:ReadWriteHosts"],
                ReadServerList = Configuration?["RedisConfig:ReadWriteHosts"],
                MaxWritePoolSize = Convert.ToInt32(Configuration?["RedisConfig:MaxWritePoolSize"]),
                MaxReadPoolSize = Convert.ToInt32(Configuration?["RedisConfig:MaxReadPoolSize"]),
                LocalCacheTime = Convert.ToInt32(Configuration?["RedisConfig:LocalCacheTime"]),
                AutoStart = Convert.ToBoolean(Configuration?["RedisConfig:AutoStart"]),
                RecordeLog = Convert.ToBoolean(Configuration?["RedisConfig:RecordeLog"]),
                DefaultDb = Convert.ToInt64(Configuration?["RedisConfig:DefaultDb"])
            };
        }
    }
}
