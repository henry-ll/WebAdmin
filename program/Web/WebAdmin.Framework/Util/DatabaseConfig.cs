using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Framework.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// 
        /// </summary>
        private static IConfiguration? configuration = null;
        /// <summary>
        /// 
        /// </summary>
        public static string? baseDConnString;
        /// <summary>
        /// 
        /// </summary>
        public static List<SlaveConnectionConfig>? Slave;
        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration Configuration
        {
            get => configuration;
            set
            {
                configuration = value;
                string? baseDbConnStr = configuration?["BaseDb"];
                bool isOpen = Convert.ToBoolean(Convert.ToInt32(configuration?["SlaveConfig:IsOpen"]));
                string? slave = configuration?["SlaveConfig:SlaveDb"];
                if (isOpen)
                    Slave = new List<SlaveConnectionConfig>{new SlaveConnectionConfig { HitRate = 10, ConnectionString = slave } };
                SetDb(baseDbConnStr, isOpen, Slave);
            }
        }
        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="baseDbconnStr">主库连接字符串</param>
        /// <param name="isOpen">是否开启读写分离</param>
        /// <param name="slave">读写分离数据库配置list</param>
        public static void SetDb(string baseDbConnStr, bool isOpen, List<SlaveConnectionConfig> slave)
        {
            baseDConnString = baseDbConnStr;
            if (isOpen)
                Slave = slave;
        }
    }
}
