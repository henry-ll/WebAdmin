using SqlSugar;
using SugarRedis;

namespace SqlSugar.Sharding.Redis
{
    /// <summary>
    /// Redis缓存类
    /// </summary>
    public class RedisCache : ICacheService
    {
        /// <summary>
        /// guid主键
        /// </summary>
        public static string guid = Guid.NewGuid().ToString();
        /// <summary>
        /// NUGET安装 SugarRedis  
        /// 注意:SugarRedis 不要扔到构造函数里面，一定要单例模式  
        /// </summary>
        public static SugarRedisClient? sugarRedisClient = null;
        /// <summary>
        /// 
        /// </summary>
        public static SugarRedisClient _service
        {
            get
            {
                if (sugarRedisClient != null)
                    return sugarRedisClient;
                var configuration = RedisConfig.Configuration;
                if (configuration != null)
                {
                    try
                    {
                        string? hoststr = configuration?["RedisConfig:ReadWriteHosts"];
                        string? db = configuration?["RedisConfig:DefaultDb"];
                        string? host = hoststr?.Split('@')[1];
                        string? pass = hoststr?.Split('@')[0];
                        sugarRedisClient = new SugarRedisClient(host + ",password=" + pass + ",connectTimeout=3000,connectRetry=1,syncTimeout=10000,DefaultDatabase=" + db);
                    }
                    catch (Exception)
                    {
                        sugarRedisClient = new SugarRedisClient();
                    }
                }
                else
                    sugarRedisClient = new SugarRedisClient();
                return sugarRedisClient;
            }
        }
        /// <summary>
        ///+1重载 new SugarRedisClient(字符串)
        ///默认:127.0.0.1:6379,password=,connectTimeout=3000,connectRetry=1,syncTimeout=10000,DefaultDatabase=0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add<T>(string key, T value)
        {
            key = guid + key;
            _service.Set(key, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheDurationInSeconds"></param>
        public void Add<T>(string key, T value, int cacheDurationInSeconds)
        {
            key = guid + key;
            _service.Set(key, value, cacheDurationInSeconds);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey<T>(string key)
        {
            return _service.Exists(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            key = guid + key;
            return _service.Get<T>(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<string> GetAllKey<T>()
        {
            return _service.SearchCacheRegex(guid + "SqlSugarDataCache.*");
        }
        /// <summary>
        /// 获取或创建指定key值的Redis缓存
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="cacheKey">key值</param>
        /// <param name="create">Func<T></param>
        /// <param name="cacheDurationInSeconds">缓存时间：（单位：秒），默认int.MaxValu</param>
        /// <returns></returns>
        public T GetOrCreate<T>(string cacheKey, Func<T> create, int cacheDurationInSeconds = int.MaxValue)
        {
            if (this.ContainsKey<T>(cacheKey))
            {
                var result = this.Get<T>(cacheKey);
                if (result == null)
                    return create();
                else
                    return result;
            }
            else
            {
                var result = create();
                this.Add(cacheKey, result, cacheDurationInSeconds);
                return result;
            }
        }
        /// <summary>
        /// 移除指定key的缓存
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">key值</param>
        public void Remove<T>(string key)
        {
            _service.Remove(key);
        }
    }
}
