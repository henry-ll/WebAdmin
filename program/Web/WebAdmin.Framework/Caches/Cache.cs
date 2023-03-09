using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace WebAdmin.Framework.Caches
{
    /// <summary>
    /// 缓存操作
    /// </summary>
    public class Cache : ICache
    {
        private static readonly MemoryCache _Cache = new(new MemoryCacheOptions());
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            if (_Cache.Get(cacheKey) != null)
                return (T)_Cache.Get(cacheKey);
            return default;
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            _Cache.Set(cacheKey, value, DateTime.Now.AddMinutes(10));
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            _Cache.Set(cacheKey, value, expireTime);
        }
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveCache(string cacheKey)
        {
            _Cache.Remove(cacheKey);
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveCache()
        {
            IList<string> keys = GetCacheKeys();
            foreach (var key in keys)
                _Cache.Remove(key);
        }
        /// <summary>
        /// 获取所有缓存键
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCacheKeys()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var entries = _Cache.GetType().GetField("_entries", flags).GetValue(_Cache);
            var keys = new List<string>();
            if (entries is not IDictionary cacheItems)
                return keys;
            foreach (DictionaryEntry cacheItem in cacheItems)
                keys.Add(cacheItem.Key.ToString());
            return keys;
        }
    }
}
