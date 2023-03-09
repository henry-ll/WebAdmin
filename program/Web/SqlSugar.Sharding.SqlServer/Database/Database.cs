using SqlSugar;
using SqlSugar.Sharding.Redis;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using WebAdmin.Domain.EntityBase;
using WebAdmin.Framework.Providers;
using X.PagedList;

namespace SqlSugar.Sharding.SqlServer
{
    public class Database : IDatabase
    {
        ///// <summary>
        ///// 构造方法
        ///// </summary>
        public Database(string connString, DbType dbType, List<SlaveConnectionConfig> slave, bool openCache = true)
        {
            if (dbContext == null)
            {
                ConnectionConfig config = new ConnectionConfig();
                var ConfigureExternalService = new ConfigureExternalServices();
                config.DbType = dbType;
                config.ConnectionString = connString;
                config.IsAutoCloseConnection = true;
                if (openCache)
                {
                    config.MoreSettings = new ConnMoreSettings()
                    {
                        IsAutoRemoveDataCache = true
                    };
                    ICacheService myCache = new RedisCache();
                    ConfigureExternalService.DataInfoCacheService = myCache;
                }
                //注意:  这儿AOP设置不能少
                ConfigureExternalService.EntityService = (c, p) =>
                {
                    // int?  decimal?这种 isnullable=true
                    if (c.PropertyType.IsGenericType && c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        p.IsNullable = true;
                };
                ConfigureExternalService.EntityNameService = (type, entity) =>
                {
                    entity.IsDisabledDelete = true; //禁止删除列
                    // entity.IsDisabledUpdateAll= true;//禁止更新列
                };
                config.ConfigureExternalServices = ConfigureExternalService;
                if (slave != null)
                {
                    config.InitKeyType = InitKeyType.Attribute;
                    config.SlaveConnectionConfigs = slave;
                }
                dbContext = new SqlSugarClient(config);
                dbContext.Aop.OnLogExecuting = (sql, pars) => //SQL执行前
                {
                    //超长sql性能有影响
                    var sqlstatement = UtilMethods.GetSqlString(DbType.SqlServer, sql, pars);
                    Console.Write(sqlstatement + "\r\n");
                    //sqlstatement = sqlstatement.Replace("'","\"");
                    string? userId = OperatorProvider.Provider.MvcCurrent()?.UserId;
                    if (string.IsNullOrWhiteSpace(userId))
                        userId = OperatorProvider.Provider.ApiCurrent()?.UserId;
                    string? account = OperatorProvider.Provider.MvcCurrent()?.Account;
                    if (string.IsNullOrWhiteSpace(account))
                        account = OperatorProvider.Provider.ApiCurrent()?.Account;
                    if (string.IsNullOrWhiteSpace(OperatorProvider.Provider.MvcCurrent()?.UserId) && string.IsNullOrWhiteSpace(OperatorProvider.Provider.ApiCurrent()?.UserId))
                    {
                        userId = "登录行为";
                        account = "无用户信息";
                    }
                    AdoDbHelper adoDbHelper = new AdoDbHelper(connString);
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append($" INSERT INTO [dbo].[Log_DBlogs] ");
                    strSql.Append($" ([Id], [OperateUserId], [OperateAccount], [SqlExecute], [LogTime]) ");
                    strSql.Append($" VALUES ");
                    strSql.Append($" (@Id, @OperateUserId, @account, @SqlExecute, @LogTime); ");
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@Id", Guid.NewGuid().ToString()));
                    parameters.Add(new SqlParameter("@OperateUserId", userId ?? ""));
                    parameters.Add(new SqlParameter("@account", account ?? ""));
                    parameters.Add(new SqlParameter("@SqlExecute", sqlstatement ?? ""));
                    parameters.Add(new SqlParameter("@LogTime", DateTime.Now));
                    adoDbHelper.ExecuteSql(strSql.ToString(), parameters.ToArray());
                };
            }
        }
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        public SqlSugarClient dbContext { get; set; }
        public SqlSugarClient SqlSugarClient()
        {
            return dbContext;
        }
        public IDatabase Db()
        {
            return this;
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">key</param>
        public void RemoveCache(string key)
        {
            dbContext.DataCache.RemoveDataCache(key);
        }

        #region 事务
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public IDatabase BeginTran()
        {
            dbContext.BeginTran();
            return this;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            dbContext.CommitTran();
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            dbContext.RollbackTran();
        }


        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public async Task<IDatabase> BeginTranAsync()
        {
            await dbContext.BeginTranAsync();
            return this;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public async Task CommitAsync()
        {
            await dbContext.CommitTranAsync();
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public async Task RollbackAsync()
        {
            await dbContext.RollbackTranAsync();
        }

        #endregion

        #region 插入
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entities">IEnumerable<T></param>
        /// <returns>0插入成功，-1出现错误</returns>
        public int Insert<T>(IEnumerable<T> entities) where T : class, new()
        {
            return dbContext.Insertable<T>(entities).ExecuteCommand();
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Insert<T>(T entity) where T : class, new()
        {
            return dbContext.Insertable<T>(entity).ExecuteCommand();
        }

        public int SpInsert<T>(IEnumerable<T> entities) where T : class, new()
        {
            return dbContext.Insertable<T>(entities).SplitTable().ExecuteCommand();
        }
        public int SpInsert<T>(T entity) where T : class, new()
        {
            return dbContext.Insertable<T>(entity).SplitTable().ExecuteCommand();
        }

        /// <summary>
        /// DataTable 保存
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键名称</param>
        /// <returns></returns>
        public int TableSave(DataTable dt, string tableName, string pkName)
        {
            dt.TableName = tableName; //设置表名
            var x = dbContext.Storageable(dt).WhereColumns(pkName).ToStorage();//id作为主键
            //x.AsInsertable.IgnoreColumns("id").ExecuteCommand();//如果是自增要添加IgnoreColumns           
            return x.AsUpdateable.ExecuteCommand();
        }



        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entities">IEnumerable<T></param>
        /// <returns>0插入成功，-1出现错误</returns>
        public async Task<int> InsertAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            return await dbContext.Insertable<T>(entities).ExecuteCommandAsync();
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync<T>(T entity) where T : class, new()
        {
            return await dbContext.Insertable<T>(entity).ExecuteCommandAsync();
        }

        public async Task<int> SpInsertAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            return await dbContext.Insertable<T>(entities).SplitTable().ExecuteCommandAsync();
        }
        public async Task<int> SpInsertAsync<T>(T entity) where T : class, new()
        {
            return await dbContext.Insertable<T>(entity).SplitTable().ExecuteCommandAsync();
        }

        /// <summary>
        /// DataTable 保存
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键名称</param>
        /// <returns></returns>
        public async Task<int> TableSaveAsync(DataTable dt, string tableName, string pkName)
        {
            dt.TableName = tableName; //设置表名
            var x = dbContext.Storageable(dt).WhereColumns(pkName).ToStorage();//id作为主键
            //x.AsInsertable.IgnoreColumns("id").ExecuteCommand();//如果是自增要添加IgnoreColumns           
            return await x.AsUpdateable.ExecuteCommandAsync();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除 整张表数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public int Delete<T>() where T : class, new()
        {
            return dbContext.Deleteable<T>().ExecuteCommand();
        }
        /// <summary>
        /// 通过主键Id删除数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public int Delete<T>(string keyValue) where T : class, new()
        {
            return dbContext.Deleteable<T>(keyValue).ExecuteCommand();
        }
        /// <summary>
        /// 删除指定的主键数组数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="keyValueArr">主键数组</param>
        /// <returns></returns>
        public int Delete<T>(string[] keyValueArr) where T : class, new()
        {
            return dbContext.Deleteable<T>(keyValueArr).ExecuteCommand();
        }
        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Delete<T>(T entity) where T : class, new()
        {
            return dbContext.Deleteable<T>(entity).ExecuteCommand();
        }
        /// <summary>
        /// 删除指定 实体list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entities">实体list</param>
        /// <returns></returns>
        public int Delete<T>(IEnumerable<T> entities) where T : class, new()
        {
            return dbContext.Deleteable<T>(entities).ExecuteCommand();
        }
        /// <summary>
        /// 删除 符合linq表达式条件的数据  
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Deleteable<T>().Where(condition).ExecuteCommand();
        }

        public int SpDelete<T>() where T : class, new()
        {
            return dbContext.Deleteable<T>().SplitTable().ExecuteCommand();
        }
        public int SpDelete<T>(string keyValue) where T : class, new()
        {
            return dbContext.Deleteable<T>(keyValue).SplitTable().ExecuteCommand();
        }
        public int SpDelete<T>(string[] keyValue) where T : class, new()
        {
            return dbContext.Deleteable<T>(keyValue).SplitTable().ExecuteCommand();
        }
        public int SpDelete<T>(T entity) where T : class, new()
        {
            return dbContext.Deleteable<T>(entity).SplitTable().ExecuteCommand();
        }
        public int SpDelete<T>(IEnumerable<T> entities) where T : class, new()
        {
            return dbContext.Deleteable<T>(entities).SplitTable().ExecuteCommand();
        }
        public int SpDelete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Deleteable<T>().Where(condition).SplitTable().ExecuteCommand();
        }




        /// <summary>
        /// 删除 整张表数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>() where T : class, new()
        {
            return await dbContext.Deleteable<T>().ExecuteCommandAsync();
        }
        /// <summary>
        /// 通过主键Id删除数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(string keyValue) where T : class, new()
        {
            return await dbContext.Deleteable<T>(keyValue).ExecuteCommandAsync();
        }
        /// <summary>
        /// 删除指定的主键数组数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="keyValueArr">主键数组</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(string[] keyValueArr) where T : class, new()
        {
            return await dbContext.Deleteable<T>(keyValueArr).ExecuteCommandAsync();
        }
        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(T entity) where T : class, new()
        {
            return await dbContext.Deleteable<T>(entity).ExecuteCommandAsync();
        }
        /// <summary>
        /// 删除指定 实体list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entities">实体list</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            return await dbContext.Deleteable<T>(entities).ExecuteCommandAsync();
        }
        /// <summary>
        /// 删除 符合linq表达式条件的数据  
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Deleteable<T>().Where(condition).ExecuteCommandAsync();
        }

        public async Task<int> SpDeleteAsync<T>() where T : class, new()
        {
            return await dbContext.Deleteable<T>().SplitTable().ExecuteCommandAsync();
        }
        public async Task<int> SpDeleteAsync<T>(string keyValue) where T : class, new()
        {
            return await dbContext.Deleteable<T>(keyValue).SplitTable().ExecuteCommandAsync();
        }
        public async Task<int> SpDeleteAsync<T>(string[] keyValue) where T : class, new()
        {
            return await dbContext.Deleteable<T>(keyValue).SplitTable().ExecuteCommandAsync();
        }
        public async Task<int> SpDeleteAsync<T>(T entity) where T : class, new()
        {
            return await dbContext.Deleteable<T>(entity).SplitTable().ExecuteCommandAsync();
        }
        public async Task<int> SpDeleteAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            return await dbContext.Deleteable<T>(entities).SplitTable().ExecuteCommandAsync();
        }
        public async Task<int> SpDeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Deleteable<T>().Where(condition).SplitTable().ExecuteCommandAsync();
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="IsAll">是否更新全部字段</param>
        /// <returns></returns>
        public int Update<T>(T entity, bool IsAll = false) where T : class, new()
        {
            int ret = 0;
            if (!IsAll)
                ret = dbContext.Updateable<T>(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand();//更新忽略null字段
            else
                ret = dbContext.Updateable<T>(entity).ExecuteCommand();
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entities">实体list</param>
        /// <param name="IsAll">是否更新全部字段</param>
        /// <returns></returns>
        public int Update<T>(IEnumerable<T> entities, bool IsAll = false) where T : class, new()
        {
            int ret = 0;
            if (!IsAll)
            {
                try
                {
                    dbContext.BeginTran();
                    foreach (T item in entities)
                        ret += dbContext.Updateable<T>(item).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand();//更新忽略null字段
                    dbContext.CommitTran();
                }
                catch (Exception ex)
                {
                    dbContext.RollbackTran();
                    ret = 0;
                }
            }
            else
                ret = dbContext.Updateable<T>(entities).ExecuteCommand();
            return ret;
        }

        public int SpUpdate<T>(T entity, bool all = false) where T : class, new()
        {
            int ret = 0;
            if (!all)
                ret = dbContext.Updateable<T>(entity).IgnoreColumns(ignoreAllNullColumns: true).SplitTable().ExecuteCommand();
            else
                ret = dbContext.Updateable<T>(entity).SplitTable().ExecuteCommand();
            return ret;
        }
        public int SpUpdate<T>(IEnumerable<T> entities, bool all = false) where T : class, new()
        {
            int ret = 0;
            if (!all)
            {
                try
                {
                    dbContext.BeginTran();
                    foreach (T item in entities)
                        ret += dbContext.Updateable<T>(item).IgnoreColumns(ignoreAllNullColumns: true).SplitTable().ExecuteCommand();
                    dbContext.CommitTran();
                }
                catch (Exception)
                {
                    dbContext.RollbackTran();
                    ret = 0;
                }
            }
            else
                ret = dbContext.Updateable<T>(entities).SplitTable().ExecuteCommand();
            return ret;
        }



        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="IsAll">是否更新全部字段</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync<T>(T entity, bool IsAll = false) where T : class, new()
        {
            int ret = 0;
            if (!IsAll)
                ret = await dbContext.Updateable<T>(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();//更新忽略null字段
            else
                ret = await dbContext.Updateable<T>(entity).ExecuteCommandAsync();
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entities">实体list</param>
        /// <param name="IsAll">是否更新全部字段</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync<T>(IEnumerable<T> entities, bool IsAll = false) where T : class, new()
        {
            int ret = 0;
            if (!IsAll)
            {
                try
                {
                    await dbContext.BeginTranAsync();
                    foreach (T item in entities)
                        ret += await dbContext.Updateable<T>(item).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommandAsync();//更新忽略null字段
                    dbContext.CommitTran();
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackTranAsync();
                    ret = 0;
                }
            }
            else
                ret = await dbContext.Updateable<T>(entities).ExecuteCommandAsync();
            return ret;
        }

        public async Task<int> SpUpdateAsync<T>(T entity, bool all = false) where T : class, new()
        {
            int ret = 0;
            if (!all)
                ret = await dbContext.Updateable<T>(entity).IgnoreColumns(ignoreAllNullColumns: true).SplitTable().ExecuteCommandAsync();
            else
                ret = await dbContext.Updateable<T>(entity).SplitTable().ExecuteCommandAsync();
            return ret;
        }
        public async Task<int> SpUpdateAsync<T>(IEnumerable<T> entities, bool all = false) where T : class, new()
        {
            int ret = 0;
            if (!all)
            {
                try
                {
                    await dbContext.BeginTranAsync();
                    foreach (T item in entities)
                        ret += await dbContext.Updateable<T>(item).IgnoreColumns(ignoreAllNullColumns: true).SplitTable().ExecuteCommandAsync();
                    await dbContext.CommitTranAsync();
                }
                catch (Exception)
                {
                    await dbContext.RollbackTranAsync();
                    ret = 0;
                }
            }
            else
                ret =await dbContext.Updateable<T>(entities).SplitTable().ExecuteCommandAsync();
            return ret;
        }
        #endregion

        #region 存储过程
        /// <summary>
        /// 执行存储过程返回dataset
        /// </summary>
        /// <param name="strSql">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>dataset</returns>
        public DataSet RunProcedureDataSet(string strSql, SugarParameter[] parameters)
        {
            return dbContext.Ado.UseStoredProcedure().GetDataSetAll(strSql, parameters);
        }
        /// <summary>
        /// //执行存储过程返回受影响的行数
        /// </summary>
        /// <param name="strSql">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>int</returns>
        public int RunProcedure(string strSql, SugarParameter[] parameters)
        {
            return dbContext.Ado.UseStoredProcedure().GetInt(strSql, parameters);
        }
        /// <summary>
        /// 执行存储过程返回list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> RunProcedureList<T>(string strSql, SugarParameter[] parameters)
        {
            return dbContext.Ado.UseStoredProcedure().SqlQuery<T>(strSql, parameters);
        }



        /// <summary>
        /// 执行存储过程返回dataset
        /// </summary>
        /// <param name="strSql">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>dataset</returns>
        public async Task<DataSet> RunProcedureDataSetAsync(string strSql, SugarParameter[] parameters)
        {
            return await dbContext.Ado.UseStoredProcedure().GetDataSetAllAsync(strSql, parameters);
        }
        /// <summary>
        /// //执行存储过程返回受影响的行数
        /// </summary>
        /// <param name="strSql">存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns>int</returns>
        public async Task<int> RunProcedureAsync(string strSql, SugarParameter[] parameters)
        {
            return await dbContext.Ado.UseStoredProcedure().GetIntAsync(strSql, parameters);
        }
        /// <summary>
        /// 执行存储过程返回list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> RunProcedureListAsync<T>(string strSql, SugarParameter[] parameters)
        {
            return await dbContext.Ado.UseStoredProcedure().SqlQueryAsync<T>(strSql, parameters);
        }
        #endregion

        #region Ado.Net
        /// <summary>
        ///  执行带参数sql l 返回DataSet集合
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回DataSet</returns>
        public DataSet FindDataSet(string strSql, SugarParameter[] parameters)
        {
            return dbContext.Ado.GetDataSetAll(strSql, parameters);
        }
        /// <summary>
        ///  执行sql 返回DataTable
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回DataTable</returns>
        public DataTable FindTable(string strSql)
        {
            return dbContext.Ado.GetDataTable(strSql);
        }
        /// <summary>
        ///  执行sql 带参数  返回DataTable 
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public DataTable FindTable(string strSql, SugarParameter[] parameters)
        {
            return dbContext.Ado.GetDataTable(strSql, parameters);
        }
        /// <summary>
        /// 执行sql 不带参数 并分页 指定排序方式  返回DataTable 
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderbyField">OrderBy排序字段（例如：CreateDate 或者 CreateDate ASC/DESC）</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页多少数据</param>
        /// <param name="total">数据总行数</param>
        /// <returns></returns>
        public DataTable FindTable(string strSql, string orderbyField, bool isAsc, int pageIndex, int pageSize, out int total)
        {
            return FindTable(strSql, null, orderbyField, isAsc, pageIndex, pageSize, out total);
        }
        /// <summary>
        /// 执行sql 带参数 并分页 指定排序方式  返回DataTable 
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <param name="orderbyField">OrderBy排序字段（例如：CreateDate 或者 CreateDate ASC/DESC）</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public DataTable FindTable(string strSql, SugarParameter[] parameters, string orderbyField, bool isAsc, int pageIndex, int pageSize, out int total)
        {
            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
                pageIndex = 1;
            int num = (pageIndex - 1) * pageSize;
            int num1 = pageIndex * pageSize;
            string OrderBy = "";
            if (!string.IsNullOrEmpty(orderbyField))
            {
                if (orderbyField.ToUpper().IndexOf("ASC") + orderbyField.ToUpper().IndexOf("DESC") > 0)
                    OrderBy = "ORDER BY " + orderbyField;
                else
                    OrderBy = "ORDER BY " + orderbyField + " " + (isAsc ? "ASC" : "DESC");
            }
            else
                OrderBy = "ORDER BY (SELECT 0)";
            sb.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER (" + OrderBy.ToSqlFilter() + ")");
            sb.Append(" As rowNum, * FROM (" + strSql + ") AS T ) AS N WHERE rowNum > " + num + " AND rowNum <= " + num1 + "");
            total = Convert.ToInt32(dbContext.Ado.GetDataTable("SELECT COUNT(1) FROM (" + strSql.ToSqlFilter() + ") AS t", parameters).Rows[0][0]);
            var list = dbContext.Ado.GetDataTable(sb.ToString(), parameters);
            return list;
        }


        /// <summary>
        ///  执行带参数sql l 返回DataSet集合
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回DataSet</returns>
        public async Task<DataSet> FindDataSetAsync(string strSql, SugarParameter[] parameters)
        {
            return await dbContext.Ado.GetDataSetAllAsync(strSql, parameters);
        }
        /// <summary>
        ///  执行sql 返回DataTable
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回DataTable</returns>
        public async Task<DataTable> FindTableAsync(string strSql)
        {
            return await dbContext.Ado.GetDataTableAsync(strSql);
        }
        /// <summary>
        ///  执行sql 带参数  返回DataTable 
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>DataTable</returns>
        public async Task<DataTable> FindTableAsync(string strSql, SugarParameter[] parameters)
        {
            return await dbContext.Ado.GetDataTableAsync(strSql, parameters);
        }
        #endregion

        #region 获取实体
        /// <summary>
        /// 通过主键Id 查询实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        public T FindEntity<T>(object keyValue) where T : class
        {
            return dbContext.Queryable<T>().InSingle(keyValue);
        }
        /// <summary>
        /// 通过 linq表达式条件 查询实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <returns></returns>
        public T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Queryable<T>().First(condition);
        }
        /// <summary>
        /// 通过  linq表达式条件 查询实体 并 按指定字段排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <param name="orderby">排序字段</param>
        /// <returns></returns>
        public T FindEntity<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new()
        {
            return dbContext.Queryable<T>().OrderBy(orderby.ToSqlFilter()).First(condition);
        }
        /// <summary>
        /// 通过  linq表达式条件 查询实体 并 按指定字段 和指定 升/降序 排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="type">OrderByType枚举：Asc, Desc</param>
        /// <returns></returns>
        public T FindEntity<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return dbContext.Queryable<T>().OrderBy(orderby, type).First(condition);
        }


        /// <summary>
        /// 通过主键Id 查询实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        public async Task<T> FindEntityAsync<T>(object keyValue) where T : class
        {
            return await dbContext.Queryable<T>().InSingleAsync(keyValue);
        }
        /// <summary>
        /// 通过 linq表达式条件 查询实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <returns></returns>
        public async Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Queryable<T>().FirstAsync(condition);
        }
        /// <summary>
        /// 通过  linq表达式条件 查询实体 并 按指定字段排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <param name="orderby">排序字段</param>
        /// <returns></returns>
        public async Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new()
        {
            return await dbContext.Queryable<T>().OrderBy(orderby.ToSqlFilter()).FirstAsync(condition);
        }
        /// <summary>
        /// 通过  linq表达式条件 查询实体 并 按指定字段 和指定 升/降序 排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="type">OrderByType枚举：Asc, Desc</param>
        /// <returns></returns>
        public async Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await dbContext.Queryable<T>().OrderBy(orderby, type).FirstAsync(condition);
        }
        #endregion

        #region 获取长度
        /// <summary>
        /// 查询 指定表的数据总行数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public int FindCount<T>() where T : class, new()
        {
            return dbContext.Queryable<T>().Count();
        }
        /// <summary>
        /// 通过 linq表达式条件 查询指定表的数据总行数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int FindCount<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Queryable<T>().Count(condition);
        }
        public int FindCount<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return dbContext.Queryable<T>().SplitTable(split).Count(condition);
        }



        /// <summary>
        /// 查询 指定表的数据总行数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public async Task<int> FindCountAsync<T>() where T : class, new()
        {
            return await dbContext.Queryable<T>().CountAsync();
        }
        /// <summary>
        /// 通过 linq表达式条件 查询指定表的数据总行数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<int> FindCountAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Queryable<T>().CountAsync(condition);
        }
        public async Task<int> FindCountAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return await dbContext.Queryable<T>().SplitTable(split).CountAsync(condition);
        }
        #endregion

        #region 延迟查询
        /// <summary>
        /// 延迟查询实体list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public ISugarQueryable<T> Queryable<T>() where T : class, new()
        {
            return dbContext.Queryable<T>();
        }
        #endregion

        #region 实体集合
        /// <summary>
        /// 查询指定实体list数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>() where T : class, new()
        {
            return dbContext.Queryable<T>().ToList();
        }
        /// <summary>
        /// 通过 linq表达式条件 查询实体list 并按指定 升/降序 排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="orderby">linq表达式条件 排序字段</param>
        /// <param name="type">OrderByType枚举：Asc, Desc</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return dbContext.Queryable<T>().OrderBy(orderby, type).ToList();
        }
        /// <summary>
        /// 通过 linq表达式条件查询实体list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式条件</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return dbContext.Queryable<T>().Where(condition).ToList();
        }
        /// <summary>
        /// 通过 linq表达式条件 排序字段 查询list数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式条件</param>
        /// <param name="orderby">排序字段</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new()
        {
            return dbContext.Queryable<T>().Where(condition).OrderBy(orderby.ToSqlFilter()).ToList();
        }
        /// <summary>
        /// 通过  linq表达式orderby条件 获取数据list 返回总行数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">linq表达式条件</param>
        /// <param name="orderby">linq表达式orderby条件</param>
        /// <param name="type">OrderByType枚举：Asc, Desc</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return dbContext.Queryable<T>().Where(condition).OrderBy(orderby, type).ToList();
        }

        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return dbContext.Queryable<T>().SplitTable(split).ToList();
        }
        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return dbContext.Queryable<T>().SplitTable(split).OrderBy(orderby, type).ToList();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return dbContext.Queryable<T>().Where(condition).ToList();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby) where T : class, new()
        {
            return dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderby.ToSqlFilter()).ToList();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderby, type).ToList();
        }






        /// <summary>
        /// 查询指定实体list数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindListAsync<T>() where T : class, new()
        {
            return await dbContext.Queryable<T>().ToListAsync();
        }
        /// <summary>
        /// 通过 linq表达式条件 查询实体list 并按指定 升/降序 排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="orderby">linq表达式条件 排序字段</param>
        /// <param name="type">OrderByType枚举：Asc, Desc</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await dbContext.Queryable<T>().OrderBy(orderby, type).ToListAsync();
        }
        /// <summary>
        /// 通过 linq表达式条件查询实体list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式条件</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await dbContext.Queryable<T>().Where(condition).ToListAsync();
        }
        /// <summary>
        /// 通过 linq表达式条件 排序字段 查询list数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式条件</param>
        /// <param name="orderby">排序字段</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new()
        {
            return await dbContext.Queryable<T>().Where(condition).OrderBy(orderby.ToSqlFilter()).ToListAsync();
        }
        /// <summary>
        /// 通过  linq表达式orderby条件 获取数据list 返回总行数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition">linq表达式条件</param>
        /// <param name="orderby">linq表达式orderby条件</param>
        /// <param name="type">OrderByType枚举：Asc, Desc</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await dbContext.Queryable<T>().Where(condition).OrderBy(orderby, type).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return await dbContext.Queryable<T>().SplitTable(split).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return  await dbContext.Queryable<T>().SplitTable(split).OrderBy(orderby, type).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return await dbContext.Queryable<T>().Where(condition).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby) where T : class, new()
        {
            return await dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderby.ToSqlFilter()).ToListAsync();
        }
        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderby, type).ToListAsync();
        }
        #endregion

        #region 实体分页
        /// <summary>
        /// 通过  linq表达式orderby条件 获取分页 数据list 返回总行数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="orderby">linq表达式orderby条件</param>
        /// <param name="type">OrderByType枚举：Asc, Desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <param name="total">数据总行数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class
        {
            int sum = 0;
            var list = dbContext.Queryable<T>().OrderBy(orderby, type).ToPageList(pageIndex, pageSize, ref sum);
            total = sum;
            return list;
        }
        /// <summary>
        /// 通过 linq表达式条件 加 指定排序字段 和分页条件 查询 实体list 并返回总数据行数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式条件</param>
        /// <param name="orderbyField">OrderBy 排序字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="total">数据总条数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderbyField, int pageIndex, int pageSize, out int total) where T : class
        {
            int totals = 0;
            var list = dbContext.Queryable<T>().Where(condition).OrderBy(orderbyField.ToSqlFilter()).ToPageList(pageIndex, pageSize, ref totals);
            total = totals;
            return list;
        }
        /// <summary>
        /// 通过 linq表达式条件 linq表达式orderby条件 获取分页 数据list 返回总行数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式条件</param>
        /// <param name="orderby">linq表达式orderby条件</param>
        /// <param name="type">OrderByType枚举：Asc, Desc</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <param name="total">数据总行数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class
        {
            int sum = 0;
            var list = dbContext.Queryable<T>().Where(condition).OrderBy(orderby, type).ToPageList(pageIndex, pageSize, ref sum);
            total = sum;
            return list;
        }
        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class
        {
            int re = 0;
            var list = dbContext.Queryable<T>().OrderBy(orderby, type).ToPageList(pageIndex, pageSize, ref re);
            total = re;
            return list;
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize, out int total) where T : class
        {
            int totals = 0;
            var list = dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderField.ToSqlFilter()).ToPageList(pageIndex, pageSize, ref totals);
            total = totals;
            return list;
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class
        {
            int re = 0;
            var list = dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderby, type).ToPageList(pageIndex, pageSize, ref re);
            total = re;
            return list;
        }
        #endregion

        #region sql获取实体集合
        /// <summary>
        /// 通过sql语句查询 指定实体list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql) where T : class
        {
            return dbContext.Ado.SqlQuery<T>(strSql);
        }
        /// <summary>
        /// 通过sql语句查询 指定实体list 带参数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql, SugarParameter[] parameters) where T : class
        {
            return dbContext.Ado.SqlQuery<T>(strSql, parameters);
        }

        /// <summary>
        /// 通过sql语句查询 指定实体list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindListAsync<T>(string strSql) where T : class
        {
            return await dbContext.Ado.SqlQueryAsync<T>(strSql);
        }
        /// <summary>
        /// 通过sql语句查询 指定实体list 带参数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindListAsync<T>(string strSql, SugarParameter[] parameters) where T : class
        {
            return await dbContext.Ado.SqlQueryAsync<T>(strSql, parameters);
        }
        #endregion

        #region 实体sql分页
        /// <summary>
        /// 通过 指定排序字段 和分页条件 查询 实体list 并返回总数据行数
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="orderbyField">OrderBy 排序字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页多少条数据</param>
        /// <param name="total">数据总条数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string orderbyField, int pageIndex, int pageSize, out int total) where T : class
        {
            int totals = 0;
            var list = dbContext.Queryable<T>().OrderBy(orderbyField.ToSqlFilter()).ToPageList(pageIndex, pageSize, ref totals);
            total = totals;
            return list;
        }
        /// <summary>
        /// 通过 sql语句 加 指定排序字段 和分页条件 查询 实体list 并返回总数据行数 无参数化条件
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="strSql"></param>
        /// <param name="orderField"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize, out int total) where T : class, new()
        {
            var list = FindList<T>(strSql, null, orderField, isAsc, pageIndex, pageSize, out total);
            return list;
        }
        /// <summary>
        /// 通过 sql语句 加 指定排序字段 和分页条件 查询 实体list 并返回总数据行数 带参数化条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <param name="orderField"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize, out int total) where T : class, new()
        {
            int outol = 0;
            var list = dbContext.SqlQueryable<T>(strSql).AddParameters(parameters).OrderBy((orderField + " " + (isAsc == true ? "ASC" : "DESC")).ToSqlFilter()).ToPageList(pageIndex, pageSize, ref outol);
            total = outol;
            return list;
        }
        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize, out int total) where T : class
        {
            int totals = 0;
            var list = dbContext.Queryable<T>().SplitTable(split).OrderBy(orderField.ToSqlFilter()).ToPageList(pageIndex, pageSize, ref totals);
            total = totals;
            return list;
        }
        #endregion

        #region 实体生成同步
        /// <summary>
        /// 创建实体文件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public bool CreateEntity(string tableName, string path)
        {
            try
            {
                dbContext.DbFirst.Where(tableName).SettingClassTemplate(old => { return old; })//修改old值替换
               .SettingConstructorTemplate(old => { return old; })//类构造函数
               .SettingNamespaceTemplate(old => { return old + "using SqlSugar;"; })//追加引用SqlSugar
               .SettingPropertyDescriptionTemplate(old => { return old; })//属性备注
               .SettingPropertyTemplate((columns, temp, type) =>
               {
                   var columnattribute = "\r\n           [SugarColumn({0})]";
                   List<string> attributes = new List<string>();
                   if (columns.IsPrimarykey)
                       attributes.Add("IsPrimaryKey=true");
                   if (columns.IsIdentity)
                       attributes.Add("IsIdentity=true");
                   if (attributes.Count == 0)
                       columnattribute = "";
                   return temp.Replace("{PropertyType}", type)
                               .Replace("{PropertyName}", columns.DbColumnName)
                               .Replace("{SugarColumn}", string.Format(columnattribute, string.Join(",", attributes)));
               }).CreateClassFile(path, "test");
                //dbcontext.DbFirst.Where(c => c == TableName).CreateClassFile(Path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityName">实体名称</param>
        /// <param name="isBak">是否备份</param>
        /// <returns></returns>
        public bool CreateTable(string entityName, bool isBak = false)
        {
            try
            {
                string assembleFileName = Assembly.GetExecutingAssembly().Location.Replace("SqlSugar.Sharding.SqlServer.dll", "WebAdmin.Domain.dll").Replace("file:///", "");
                Assembly ass = Assembly.LoadFrom(assembleFileName);
                var typelist = ass.GetTypes().Where(c => c.Name == entityName).ToArray();
                //dbcontext.DbMaintenance.CreateDatabase();
                if (isBak)
                    dbContext.CodeFirst.BackupTable().InitTables(typelist);
                else
                    dbContext.CodeFirst.InitTables(typelist);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region X.PagedList分页组件
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = dbContext.Queryable<T>().OrderBy(orderby, type).ToPagedList(pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, string orderbyField, int pageIndex, int pageSize) where T : class
        {
            var result = dbContext.Queryable<T>().Where(condition).OrderBy(orderbyField.ToSqlFilter()).ToPagedList(pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = dbContext.Queryable<T>().Where(condition).OrderBy(orderby, type).ToPagedList(pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = dbContext.Queryable<T>().OrderBy(orderby, type).ToPagedList(pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize) where T : class
        {
            var result = dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderField.ToSqlFilter()).ToPagedList(pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderby, type).ToPagedList(pageIndex, pageSize);
            return result;
        }

        public IPagedList<T> ToPagedList<T>(string orderbyField, int pageIndex, int pageSize) where T : class
        {
            var result = dbContext.Queryable<T>().OrderBy(orderbyField.ToSqlFilter()).ToPagedList(pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new()
        {
            var result = ToPagedList<T>(strSql, null, orderField, isAsc, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new()
        {
            var result = dbContext.SqlQueryable<T>(strSql).AddParameters(parameters).OrderBy((orderField + " " + (isAsc == true ? "ASC" : "DESC")).ToSqlFilter()).ToPagedList(pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize) where T : class
        {
            var result = dbContext.Queryable<T>().SplitTable(split).OrderBy(orderField.ToSqlFilter()).ToPagedList(pageIndex, pageSize);
            return result;
        }


        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = await dbContext.Queryable<T>().OrderBy(orderby, type).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, string orderbyField, int pageIndex, int pageSize) where T : class
        {
            var result = await dbContext.Queryable<T>().Where(condition).OrderBy(orderbyField.ToSqlFilter()).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = await dbContext.Queryable<T>().Where(condition).OrderBy(orderby, type).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = await dbContext.Queryable<T>().OrderBy(orderby, type).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize) where T : class
        {
            var result = await dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderField.ToSqlFilter()).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = await dbContext.Queryable<T>().Where(condition).SplitTable(split).OrderBy(orderby, type).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }

        public async Task<IPagedList<T>> ToPagedListAsync<T>(string orderbyField, int pageIndex, int pageSize) where T : class
        {
            var result = await dbContext.Queryable<T>().OrderBy(orderbyField.ToSqlFilter()).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new()
        {
            var result = await ToPagedListAsync<T>(strSql, null, orderField, isAsc, pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new()
        {
            var result = await dbContext.SqlQueryable<T>(strSql).AddParameters(parameters).OrderBy((orderField + " " + (isAsc == true ? "ASC" : "DESC")).ToSqlFilter()).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize) where T : class
        {
            var result = await dbContext.Queryable<T>().SplitTable(split).OrderBy(orderField.ToSqlFilter()).ToPagedListAsync(pageIndex, pageSize);
            return result;
        }
        #endregion
    }
}