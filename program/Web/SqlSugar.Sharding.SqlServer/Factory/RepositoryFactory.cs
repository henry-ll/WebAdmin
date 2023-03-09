using SqlSugar;
namespace SqlSugar.Sharding.SqlServer
{
    /// <summary>
    /// 定义仓储模型工厂
    /// </summary>
    public class RepositoryFactory
    {
        /// <summary>
        /// 定义仓储
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public IRepository BaseRepository(string connString, DbType DbType=DbType.SqlServer, List<SlaveConnectionConfig> Slave=null)
        {
            return new Repository(DbFactory.Base(connString, DbType, Slave));
        }
        /// <summary>
        /// 定义仓储(基础库)
        /// </summary>
        /// <returns></returns>
        public IRepository BaseRepository()
        {
            return new Repository(DbFactory.Base());
        }
    }
}
