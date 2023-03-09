namespace WebAdmin.Framework.Caches
{
    public class CacheByRedis : ICacheByRedis
    {
        #region Key-Value
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T Read<T>(string cacheKey, long dbId = 0) where T : class
        {
            return RedisCache.Get<T>(cacheKey, dbId);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void Write<T>(string cacheKey, T value, long dbId = 0) where T : class
        {
            RedisCache.Set(cacheKey, value, dbId);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void Write<T>(string cacheKey, T value, DateTime expireTime, long dbId = 0) where T : class
        {
            RedisCache.Set(cacheKey, value, expireTime, dbId);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="TimeSpan">缓存时间</param>
        public void Write<T>(string cacheKey, T value, TimeSpan timeSpan, long dbId = 0) where T : class
        {
            RedisCache.Set(cacheKey, value, timeSpan, dbId);
        }
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void Remove(string cacheKey, long dbId = 0)
        {
            RedisCache.Remove(cacheKey, dbId);
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveAll(long dbId = 0)
        {
            RedisCache.RemoveAll(dbId);
        }
        #endregion

        #region Key-List
        /// <summary>
        /// 获取列表缓存数据
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public List<T> ReadList<T>(string cacheKey, long dbId = 0) where T : class
        {
            return RedisCache.List_GetList<T>(cacheKey, dbId);
        }
        /// <summary>
        /// 写入列表缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void WriteList<T>(string cacheKey, T value, long dbId = 0) where T : class
        {
            RedisCache.List_Add(cacheKey, value, dbId);
        }

        /// <summary>
        /// 移除列表某个值缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveList(string cacheKey, long dbId = 0)
        {
            RedisCache.List_Remove(cacheKey, dbId);
        }
        /// <summary>
        /// 移除列表所有值
        /// </summary>
        public void RemoveListAll<T>(string cacheKey, long dbId = 0) where T : class
        {
            RedisCache.List_RemoveAll<T>(cacheKey, dbId);
        }
        #endregion

        #region -- Hash --

        public bool Hash_Exist<T>(string key, string dataKey, long dbId = 0) where T : class
        {
            return RedisCache.Hash_Exist<T>(key, dataKey, dbId);
        }

        public bool Hash_Set<T>(string key, string dataKey, T t, long dbId = 0) where T : class
        {
            return RedisCache.Hash_Set(key, dataKey, t, dbId);
        }

        public bool Hash_Remove(string key, string dataKey, long dbId = 0)
        {
            return RedisCache.Hash_Remove(key, dataKey, dbId);
        }

        public bool Hash_Remove(string key, long dbId = 0)
        {
            return RedisCache.Hash_Remove(key, dbId);
        }

        public T Hash_Get<T>(string key, string dataKey, long dbId = 0) where T : class
        {
            return RedisCache.Hash_Get<T>(key, dataKey, dbId);
        }

        public List<T> Hash_GetAll<T>(string key, long dbId = 0) where T : class
        {
            return RedisCache.Hash_GetAll<T>(key, dbId);
        }
        #endregion

    }
}