using Netnr.UAParser;
using Polly;
using SqlSugar;
using StackExchange.Redis;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Xml.Linq;
using WebAdmin.Domain.EntityBase;
using X.PagedList;
using static Netnr.UAParser.Entities;

namespace SqlSugar.Sharding.SqlServer
{
    /// <summary>
    /// 定义仓储模型中的数据标准操作
    /// </summary>
    public class Repository : IRepository
    {
        public IDatabase db;
        public Repository(IDatabase idatabase)
        {
            this.db = idatabase;
        }
        public IRepository Db()
        {
            return this;
        }
        public SqlSugarClient SqlSugarClient()
        {
            return db.SqlSugarClient();
        }
        public void RemoveCache(string Key)
        {
            db.RemoveCache(Key);
        }

        #region 事务
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public IRepository BeginTrans()
        {
            db.BeginTran();
            return this;
        }
        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            db.Commit();
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            db.Rollback();
        }
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public async Task<IRepository> BeginTranAsync()
        {
            await db.BeginTranAsync();
            return this;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public async Task CommitAsync()
        {
            await db.CommitAsync();
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public async Task RollbackAsync()
        {
            await db.RollbackAsync();
        }
        #endregion

        #region 插入
        public int Insert<T>(T entity) where T : class, new()
        {
            return db.Insert<T>(entity);
        }
        public int Insert<T>(IEnumerable<T> entity) where T : class, new()
        {
            return db.Insert<T>(entity);
        }

        public int SpInsert<T>(T entity) where T : class, new()
        {
            return db.SpInsert<T>(entity);
        }
        public int SpInsert<T>(IEnumerable<T> entity) where T : class, new()
        {
            return db.SpInsert<T>(entity);
        }
        public int TableSave(DataTable dt, string TableName, string PkName)
        {
            return db.TableSave(dt, TableName, PkName);
        }


        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync<T>(T entity) where T : class, new()
        {
            return await db.InsertAsync<T>(entity);
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entities">IEnumerable<T></param>
        /// <returns>0插入成功，-1出现错误</returns>
        public async Task<int> InsertAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            return await db.InsertAsync<T>(entities);
        }
        public async Task<int> SpInsertAsync<T>(T entity) where T : class, new()
        {
            return await db.SpInsertAsync<T>(entity);
        }
        public async Task<int> SpInsertAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            return await db.SpInsertAsync<T>(entities);
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
            return await db.TableSaveAsync(dt, tableName, pkName);
        }
        #endregion

        #region 删除
        public int Delete<T>() where T : class, new()
        {
            return db.Delete<T>();
        }
        public int Delete<T>(string keyValue) where T : class, new()
        {
            return db.Delete<T>(keyValue);
        }
        public int Delete<T>(string[] keyValue) where T : class, new()
        {
            return db.Delete<T>(keyValue);
        }
        public int Delete<T>(T entity) where T : class, new()
        {
            return db.Delete<T>(entity);
        }
        public int Delete<T>(IEnumerable<T> entity) where T : class, new()
        {
            return db.Delete<T>(entity);
        }
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.Delete<T>(condition);
        }

        public int SpDelete<T>() where T : class, new()
        {
            return db.SpDelete<T>();
        }
        public int SpDelete<T>(string keyValue) where T : class, new()
        {
            return db.SpDelete<T>(keyValue);
        }
        public int SpDelete<T>(string[] keyValue) where T : class, new()
        {
            return db.SpDelete<T>(keyValue);
        }
        public int SpDelete<T>(T entity) where T : class, new()
        {
            return db.SpDelete<T>(entity);
        }
        public int SpDelete<T>(IEnumerable<T> entity) where T : class, new()
        {
            return db.SpDelete<T>(entity);
        }
        public int SpDelete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.SpDelete<T>(condition);
        }


        /// <summary>
        /// 删除 整张表数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>() where T : class, new()
        {
            return await db.DeleteAsync<T>();
        }
        /// <summary>
        /// 通过主键Id删除数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(string keyValue) where T : class, new()
        {
            return await db.DeleteAsync<T>(keyValue);
        }
        /// <summary>
        /// 删除指定的主键数组数据
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="keyValueArr">主键数组</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(string[] keyValueArr) where T : class, new()
        {
            return await db.DeleteAsync<T>(keyValueArr);
        }
        /// <summary>
        /// 删除指定实体
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(T entity) where T : class, new()
        {
            return await db.DeleteAsync<T>(entity);
        }
        /// <summary>
        /// 删除指定 实体list
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="entities">实体list</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            return await db.DeleteAsync<T>(entities);
        }
        /// <summary>
        /// 删除 符合linq表达式条件的数据  
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="condition">linq表达式</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await db.DeleteAsync<T>(condition);
        }

        public async Task<int> SpDeleteAsync<T>() where T : class, new()
        {
            return await db.SpDeleteAsync<T>();
        }
        public async Task<int> SpDeleteAsync<T>(string keyValue) where T : class, new()
        {
            return await db.SpDeleteAsync<T>(keyValue);
        }
        public async Task<int> SpDeleteAsync<T>(string[] keyValue) where T : class, new()
        {
            return await db.SpDeleteAsync<T>(keyValue);
        }
        public async Task<int> SpDeleteAsync<T>(T entity) where T : class, new()
        {
            return await db.SpDeleteAsync<T>(entity);
        }
        public async Task<int> SpDeleteAsync<T>(IEnumerable<T> entities) where T : class, new()
        {
            return await db.SpDeleteAsync<T>(entities);
        }
        public async Task<int> SpDeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await db.SpDeleteAsync<T>(condition);
        }
        #endregion

        #region 更新
        public int Update<T>(T entity, bool all = false) where T : class, new()
        {
            return db.Update<T>(entity, all);
        }
        public int Update<T>(IEnumerable<T> entity, bool all = false) where T : class, new()
        {
            return db.Update<T>(entity, all);
        }

        public int SpUpdate<T>(T entity, bool all = false) where T : class, new()
        {
            return db.SpUpdate<T>(entity, all);
        }
        public int SpUpdate<T>(IEnumerable<T> entity, bool all = false) where T : class, new()
        {
            return db.SpUpdate<T>(entity, all);
        }


        public async Task<int> UpdateAsync<T>(T entity, bool all = false) where T : class, new()
        {
            return await db.UpdateAsync<T>(entity, all);
        }

        public async Task<int> UpdateAsync<T>(IEnumerable<T> entities, bool all = false) where T : class, new()
        {
            return await db.UpdateAsync<T>(entities, all);
        }

        public async Task<int> SpUpdateAsync<T>(T entity, bool all = false) where T : class, new()
        {
            return await db.SpUpdateAsync<T>(entity, all);
        }

        public async Task<int> SpUpdateAsync<T>(IEnumerable<T> entities, bool all = false) where T : class, new()
        {
            return await db.SpUpdateAsync<T>(entities, all);
        }
        #endregion

        #region 存储过程
        public DataSet RunProcedureDataSet(string strSql, SugarParameter[] parameters)
        {
            return db.RunProcedureDataSet(strSql, parameters);
        }
        public int RunProcedure(string strSql, SugarParameter[] parameters)
        {
            return db.RunProcedure(strSql, parameters);
        }
        public IEnumerable<T> RunProcedureList<T>(string strSql, SugarParameter[] parameters) where T : class
        {
            return db.RunProcedureList<T>(strSql, parameters);
        }

        public async Task<DataSet> RunProcedureDataSetAsync(string strSql, SugarParameter[] parameters)
        {
            return await db.RunProcedureDataSetAsync(strSql, parameters);
        }

        public async Task<int> RunProcedureAsync(string strSql, SugarParameter[] parameters)
        {
            return await db.RunProcedureAsync(strSql, parameters);
        }

        public async Task<IEnumerable<T>> RunProcedureListAsync<T>(string strSql, SugarParameter[] parameters)
        {
            return await db.RunProcedureListAsync<T>(strSql, parameters);
        }
        #endregion

        #region ADO.NET
        public DataTable FindTable(string strSql)
        {
            return db.FindTable(strSql);
        }
        public DataTable FindTable(string strSql, SugarParameter[] parameters)
        {
            return db.FindTable(strSql, parameters);
        }
        public DataTable FindTable(string strSql, Pagination pagination)
        {
            int total = pagination.Records;
            var data = db.FindTable(strSql, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public DataTable FindTable(string strSql, SugarParameter[] parameters, Pagination pagination)
        {
            int total = pagination.Records;
            var data = db.FindTable(strSql, parameters, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize, out int total)
        {
            return db.FindTable(strSql, orderField, isAsc, pageIndex, pageSize, out total);
        }
        public DataTable FindTable(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize, out int total)
        {
            return db.FindTable(strSql, parameters, orderField, isAsc, pageIndex, pageSize, out total);
        }
        public DataSet FindDataSet(string strSql, SugarParameter[] parameters)
        {
            return db.FindDataSet(strSql, parameters);
        }


        public async Task<DataSet> FindDataSetAsync(string strSql, SugarParameter[] parameters)
        {
            return await db.FindDataSetAsync(strSql, parameters);
        }

        public async Task<DataTable> FindTableAsync(string strSql)
        {
            return await db.FindTableAsync(strSql);
        }

        public async Task<DataTable> FindTableAsync(string strSql, SugarParameter[] parameters)
        {
            return await db.FindTableAsync(strSql, parameters);
        }
        #endregion

        #region 获取实体
        public T FindEntity<T>(object keyValue) where T : class, new()
        {
            return db.FindEntity<T>(keyValue);
        }
        public T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.FindEntity<T>(condition);
        }
        public T FindEntity<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new()
        {
            return db.FindEntity<T>(condition, orderby);
        }
        public T FindEntity<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return db.FindEntity<T>(condition, orderby, type);
        }


        public async Task<T> FindEntityAsync<T>(object keyValue) where T : class
        {
            return await db.FindEntityAsync<T>(keyValue);
        }

        public async Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await db.FindEntityAsync<T>(condition);
        }

        public async Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new()
        {
            return await db.FindEntityAsync<T>(condition, orderby);
        }

        public async Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await db.FindEntityAsync<T>(condition, orderby, type);
        }
        #endregion

        #region 获取长度
        public int FindCount<T>() where T : class, new()
        {
            return db.FindCount<T>();
        }
        public int FindCount<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.FindCount<T>(condition);
        }
        public int FindCount<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return db.FindCount<T>(condition, split);
        }


        public async Task<int> FindCountAsync<T>() where T : class, new()
        {
            return await db.FindCountAsync<T>();
        }

        public async Task<int> FindCountAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await db.FindCountAsync<T>(condition);
        }

        public async Task<int> FindCountAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return await db.FindCountAsync<T>(condition, split);
        }
        #endregion

        #region 延迟查询
        public ISugarQueryable<T> Queryable<T>() where T : class, new()
        {
            return db.Queryable<T>();
        }
        #endregion

        #region 实体集合
        public IEnumerable<T> FindList<T>() where T : class, new()
        {
            return db.FindList<T>();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.FindList<T>(condition);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new()
        {
            return db.FindList<T>(condition, orderby);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return db.FindList<T>(orderby, type);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return db.FindList<T>(condition, orderby, type);
        }

        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return db.FindList<T>(split);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return db.FindList<T>(condition, split);
        }
        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return db.FindList<T>(split, orderby, type);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby) where T : class, new()
        {
            return db.FindList<T>(condition, split, orderby);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return db.FindList<T>(condition, split, orderby, type);
        }


        public async Task<IEnumerable<T>> FindListAsync<T>() where T : class, new()
        {
            return await db.FindListAsync<T>();
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await db.FindListAsync<T>(orderby, type);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return await db.FindListAsync<T>(condition);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new()
        {
            return await db.FindListAsync<T>(condition, orderby);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await db.FindListAsync<T>(condition, orderby, type);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return await db.FindListAsync<T>(split);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await db.FindListAsync<T>(split, orderby, type);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new()
        {
            return await db.FindListAsync<T>(condition, split);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby) where T : class, new()
        {
            return await db.FindListAsync<T>(condition, split, orderby);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new()
        {
            return await db.FindListAsync<T>(condition, split, orderby, type);
        }
        #endregion

        #region 实体分页
        public IEnumerable<T> FindList<T>(Pagination pagination) where T : class, new()
        {
            int total = pagination.Records;
            var data = db.FindList<T>(pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Pagination pagination) where T : class, new()
        {
            int total = pagination.Records;
            var data = db.FindList<T>(condition, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class
        {
            return db.FindList<T>(orderby, type, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize, out int total) where T : class
        {
            return db.FindList<T>(orderby, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class
        {
            return db.FindList<T>(condition, orderby, type, pageIndex, pageSize, out total);
        }

        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination) where T : class, new()
        {
            int total = pagination.Records;
            var data = db.FindList<T>(split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination) where T : class, new()
        {
            int total = pagination.Records;
            var data = db.FindList<T>(condition, split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class
        {
            return db.FindList<T>(split, orderby, type, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize, out int total) where T : class
        {
            return db.FindList<T>(condition, split, orderby, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class
        {
            return db.FindList<T>(condition, split, orderby, type, pageIndex, pageSize, out total);
        }
        #endregion

        #region sql获取实体集合
        public IEnumerable<T> FindList<T>(string strSql) where T : class, new()
        {
            return db.FindList<T>(strSql);
        }

        public IEnumerable<T> FindList<T>(string strSql, SugarParameter[] parameters) where T : class, new()
        {
            return db.FindList<T>(strSql, parameters);
        }
        public async Task<IEnumerable<T>> FindListAsync<T>(string strSql) where T : class
        {
            return await db.FindListAsync<T>(strSql);
        }

        public async Task<IEnumerable<T>> FindListAsync<T>(string strSql, SugarParameter[] parameters) where T : class
        {
            return await db.FindListAsync<T>(strSql, parameters);
        }
        #endregion

        #region 实体sql分页
        public IEnumerable<T> FindList<T>(string orderField, int pageIndex, int pageSize, out int total) where T : class
        {
            return db.FindList<T>(orderField, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList<T>(string strSql, Pagination pagination) where T : class, new()
        {
            int total = pagination.Records;
            var data = db.FindList<T>(strSql, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(string strSql, SugarParameter[] parameters, Pagination pagination) where T : class, new()
        {
            int total = pagination.Records;
            var data = db.FindList<T>(strSql, parameters, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize, out int total) where T : class, new()
        {
            return db.FindList<T>(strSql, orderField, isAsc, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize, out int total) where T : class, new()
        {
            return db.FindList<T>(strSql, parameters, orderField, isAsc, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize, out int total) where T : class, new()
        {
            return db.FindList<T>(split, orderField, pageIndex, pageSize, out total);
        }
        #endregion

        #region 实体生成同步
        public bool CreateEntity(string TableName, string Path)
        {
            return db.CreateEntity(TableName, Path);
        }
        public bool CreateTable(string EntityName, bool bak = false)
        {
            return db.CreateTable(EntityName, bak);
        }
        #endregion

        #region 分页组件
        public IPagedList<T> ToPagedList<T>(Pagination pagination) where T : class
        {
            var result = db.ToPagedList<T>(pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Pagination pagination) where T : class
        {
            var result = db.ToPagedList<T>(condition, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = db.ToPagedList<T>(orderby, type, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize) where T : class
        {
            var result = db.ToPagedList<T>(orderby, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = db.ToPagedList<T>(condition, orderby, type, pageIndex, pageSize);
            return result;
        }

        public IPagedList<T> ToPagedList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination) where T : class
        {
            var result = db.ToPagedList<T>(split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination) where T : class
        {
            var result = db.ToPagedList<T>(condition, split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = db.ToPagedList<T>(split, orderby, type, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize) where T : class
        {
            var result = db.ToPagedList<T>(condition, split, orderby, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = db.ToPagedList<T>(condition, split, orderby, type, pageIndex, pageSize);
            return result;
        }

        public IPagedList<T> ToPagedList<T>(string orderField, int pageIndex, int pageSize) where T : class
        {
            return db.ToPagedList<T>(orderField, pageIndex, pageSize);
        }
        public IPagedList<T> ToPagedList<T>(string strSql, Pagination pagination) where T : class, new()
        {
            var result = db.ToPagedList<T>(strSql, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(string strSql, SugarParameter[] parameters, Pagination pagination) where T : class, new()
        {
            var result = db.ToPagedList<T>(strSql, parameters, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new()
        {
            return db.ToPagedList<T>(strSql, orderField, isAsc, pageIndex, pageSize);
        }
        public IPagedList<T> ToPagedList<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class,new()
        {
            return db.ToPagedList<T>(strSql, parameters, orderField, isAsc, pageIndex, pageSize);
        }
        public IPagedList<T> ToPagedList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize) where T : class
        {
            return db.ToPagedList<T>(split, orderField, pageIndex, pageSize);
        }



        public async Task<IPagedList<T>> ToPagedListAsync<T>(Pagination pagination) where T : class
        {
            var result =await db.ToPagedListAsync<T>(pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Pagination pagination) where T : class
        {
            var result =await db.ToPagedListAsync<T>(condition, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result =await db.ToPagedListAsync<T>(orderby, type, pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize) where T : class
        {
            var result =await db.ToPagedListAsync<T>(orderby, pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result =await db.ToPagedListAsync<T>(condition, orderby, type, pageIndex, pageSize);
            return result;
        }

        public async Task<IPagedList<T>> ToPagedListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination) where T : class
        {
            var result =await db.ToPagedListAsync<T>(split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination) where T : class
        {
            var result =await db.ToPagedListAsync<T>(condition, split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result =await db.ToPagedListAsync<T>(split, orderby, type, pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize) where T : class
        {
            var result =await db.ToPagedListAsync<T>(condition, split, orderby, pageIndex, pageSize);
            return result;
        }
        public async  Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class
        {
            var result = await db.ToPagedListAsync<T>(condition, split, orderby, type, pageIndex, pageSize);
            return result;
        }

        public async Task<IPagedList<T>> ToPagedListAsync<T>(string orderField, int pageIndex, int pageSize) where T : class
        {
            return await db.ToPagedListAsync<T>(orderField, pageIndex, pageSize);
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(string strSql, Pagination pagination) where T : class, new()
        {
            var result = await db.ToPagedListAsync<T>(strSql, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(string strSql, SugarParameter[] parameters, Pagination pagination) where T : class, new()
        {
            var result =await db.ToPagedListAsync<T>(strSql, parameters, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new()
        {
            return await db.ToPagedListAsync<T>(strSql, orderField, isAsc, pageIndex, pageSize);
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new()
        {
            return await db.ToPagedListAsync<T>(strSql, parameters, orderField, isAsc, pageIndex, pageSize);
        }
        public async Task<IPagedList<T>> ToPagedListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize) where T : class
        {
            return await db.ToPagedListAsync<T>(split, orderField, pageIndex, pageSize);
        }
        #endregion
    }
}
