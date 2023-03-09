
namespace WebAdmin.Framework.Caches
{
    /// <summary>
    /// 缓存工厂类
    /// </summary>
    public class CacheFactory
    {
        /// <summary>
        /// 缓存到内存里（自带缓存 ）
        /// </summary>
        /// <returns></returns>
        public static ICache Cache()
        {
            return new Cache();
        }
        /// <summary>
        /// 缓存到Redis
        /// </summary>
        /// <returns></returns>
        public static ICacheByRedis CacheByRedis()
        {
            return new CacheByRedis();
        }
    }
}
