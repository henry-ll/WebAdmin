using Microsoft.Extensions.Configuration;
using SqlSugar;
using WebAdmin.Framework.Util;

namespace SqlSugar.Sharding.SqlServer
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="DbType">数据库类型</param>
        /// <returns></returns>
        public static IDatabase Base(string connString, DbType DbType, List<SlaveConnectionConfig> Slave)
        {
            if (DatabaseConfig.Configuration != null && connString.IndexOf("Server=") < 0)
                connString = DatabaseConfig.Configuration[connString];
            return new Database(connString, DbType, Slave);
        }
        /// <summary>
        /// 连接基础库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Base(DbType DbType=DbType.SqlServer)
        {
            return new Database(DatabaseConfig.baseDConnString, DbType, DatabaseConfig.Slave);
        }
    }
}
