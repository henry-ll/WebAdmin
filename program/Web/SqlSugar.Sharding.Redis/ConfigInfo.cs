using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugar.Sharding.Redis
{
    /// <summary>
    /// Redis配置信息
    /// </summary>
    public class RedisConfigInfo
    {
        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        public string? WriteServerList { get; set; }
        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        public string? ReadServerList { get; set; }
        /// <summary>
        /// 最大写链接数
        /// </summary>
        public int MaxWritePoolSize { get; set; }
        /// <summary>
        /// 最大读链接数
        /// </summary>
        public int MaxReadPoolSize { get; set; }
        /// <summary>
        /// 自动重启
        /// </summary>
        public bool AutoStart { get; set; }
        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        public int LocalCacheTime { get; set; }
        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项
        /// </summary>
        public bool RecordeLog { get; set; }
        /// <summary>
        /// 默认开始db
        /// </summary>
        public long DefaultDb { get; set; }
    }
}
