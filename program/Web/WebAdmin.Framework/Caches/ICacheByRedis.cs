using System;
using System.Collections.Generic;

namespace WebAdmin.Framework.Caches
{
    /// <summary>
    /// Redis缓存接口
    /// </summary>
    public interface ICacheByRedis
    {
        #region Key-Value
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        T Read<T>(string cacheKey, long dbId = 0) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        void Write<T>(string cacheKey, T value, long dbId = 0) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        void Write<T>(string cacheKey, T value, DateTime expireTime, long dbId = 0) where T : class;
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        void Write<T>(string cacheKey, T value, TimeSpan timeSpan, long dbId = 0) where T : class;
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        void Remove(string cacheKey, long dbId = 0);
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        void RemoveAll(long dbId = 0);
        #endregion

        #region Key-List
        /// <summary>
        /// 获取列表缓存数据
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        List<T> ReadList<T>(string cacheKey, long dbId = 0) where T : class;
        /// <summary>
        /// 写入列表缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        void WriteList<T>(string cacheKey, T value, long dbId = 0) where T : class;
        /// <summary>
        /// 移除列表某个值缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        void RemoveList(string cacheKey, long dbId = 0);
        /// <summary>
        /// 移除列表所有值
        /// </summary>
        void RemoveListAll<T>(string cacheKey, long dbId = 0) where T : class;
        #endregion

        #region -- Hash --
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Hash_Exist<T>(string key, string dataKey, long dbId = 0) where T : class;
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Hash_Set<T>(string key, string dataKey, T t, long dbId = 0) where T : class;
        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Hash_Remove(string key, string dataKey, long dbId = 0);
        /// <summary>
        /// 移除整个hash
        /// </summary>
        /// <param name="key">hashID</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Hash_Remove(string key, long dbId = 0);
        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        T Hash_Get<T>(string key, string dataKey, long dbId = 0) where T : class;
        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        List<T> Hash_GetAll<T>(string key, long dbId = 0) where T : class;
        #endregion

    }
}
