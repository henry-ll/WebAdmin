using Netnr.UAParser;
using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing.Printing;
using System.Linq;
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
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private IDatabase db;
        public Repository(IDatabase database)
        {
            this.db = database;
        }
        public IRepository<T> Db()
        {
            return this;
        }
        public SqlSugarClient SqlSugarClient()
        {
            return db.SqlSugarClient();
        }
        public void RemoveCache(string key)
        {
            db.RemoveCache(key);
        }

        #region 事务
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public IRepository<T> BeginTrans()
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

        public async Task<IRepository<T>> BeginTranAsync()
        {
            await db.BeginTranAsync();
            return this;
        }

        public async Task CommitAsync()
        {
           await db.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await db.RollbackAsync();
        }

        #endregion

        #region 插入
        public int Insert(T entity)
        {
            return db.Insert<T>(entity);
        }
        public int Insert(IEnumerable<T> entity)
        {
            return db.Insert<T>(entity);
        }

        public int SpInsert(T entity)
        {
            return db.SpInsert<T>(entity);
        }
        public int SpInsert(IEnumerable<T> entity)
        {
            return db.SpInsert<T>(entity);
        }
        public int TableSave(DataTable dt, string TableName, string PkName)
        {
            return db.TableSave(dt, TableName, PkName);
        }

        public async Task<int> InsertAsync(T entity)
        {
            return await db.InsertAsync<T>(entity);
        }

        public async Task<int> InsertAsync(IEnumerable<T> entities)
        {
            return await db.InsertAsync<T>(entities);
        }

        public async Task<int> SpInsertAsync(T entity)
        {
            return await db.SpInsertAsync<T>(entity);
        }

        public async Task<int> SpInsertAsync(IEnumerable<T> entities)
        {
            return await db.SpInsertAsync<T>(entities);
        }

        public async Task<int> TableSaveAsync(DataTable dt, string tableName, string pkName)
        {
            return await db.TableSaveAsync(dt, tableName, pkName);
        }

        #endregion

        #region 删除
        public int Delete()
        {
            return db.Delete<T>();
        }
        public int Delete(string keyValue)
        {
            return db.Delete<T>(keyValue);
        }
        public int Delete(string[] keyValue)
        {
            return db.Delete<T>(keyValue);
        }
        public int Delete(T entity)
        {
            return db.Delete<T>(entity);
        }
        public int Delete(IEnumerable<T> entity)
        {
            return db.Delete<T>(entity);
        }
        public int Delete(Expression<Func<T, bool>> condition)
        {
            return db.Delete<T>(condition);
        }

        public int SpDelete()
        {
            return db.SpDelete<T>();
        }
        public int SpDelete(string keyValue)
        {
            return db.SpDelete<T>(keyValue);
        }
        public int SpDelete(string[] keyValue)
        {
            return db.SpDelete<T>(keyValue);
        }
        public int SpDelete(T entity)
        {
            return db.SpDelete<T>(entity);
        }
        public int SpDelete(IEnumerable<T> entity)
        {
            return db.SpDelete<T>(entity);
        }
        public int SpDelete(Expression<Func<T, bool>> condition)
        {
            return db.SpDelete<T>(condition);
        }


        public async Task<int> DeleteAsync()
        {
            return await db.DeleteAsync<T>();
        }

        public async Task<int> DeleteAsync(string keyValue)
        {
            return await db.DeleteAsync<T>(keyValue);
        }

        public async Task<int> DeleteAsync(string[] keyValue)
        {
            return await db.DeleteAsync<T>(keyValue);
        }

        public async Task<int> DeleteAsync(T entity)
        {
            return await db.DeleteAsync<T>(entity);
        }

        public async Task<int> DeleteAsync(IEnumerable<T> entities)
        {
            return await db.DeleteAsync<T>(entities);
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> condition)
        {
            return await db.DeleteAsync<T>(condition);
        }

        public async Task<int> SpDeleteAsync()
        {
            return await db.SpDeleteAsync<T>();
        }

        public async Task<int> SpDeleteAsync(string keyValue)
        {
            return await db.SpDeleteAsync<T>(keyValue);
        }

        public async Task<int> SpDeleteAsync(string[] keyValue)
        {
            return await db.SpDeleteAsync<T>(keyValue);
        }

        public async Task<int> SpDeleteAsync(T entity)
        {
            return await db.SpDeleteAsync<T>(entity);
        }

        public async Task<int> SpDeleteAsync(IEnumerable<T> entities)
        {
            return await db.SpDeleteAsync<T>(entities);
        }

        public async Task<int> SpDeleteAsync(Expression<Func<T, bool>> condition)
        {
            return await db.SpDeleteAsync<T>(condition);
        }
        #endregion

        #region 更新
        public int Update(T entity, bool all = false)
        {
            return db.Update<T>(entity, all);
        }
        public int Update(IEnumerable<T> entity, bool all = false)
        {
            return db.Update<T>(entity, all);
        }

        public int SpUpdate(T entity, bool all = false)
        {
            return db.SpUpdate<T>(entity, all);
        }
        public int SpUpdate(IEnumerable<T> entity, bool all = false)
        {
            return db.SpUpdate<T>(entity, all);
        }


        public async Task<int> UpdateAsync(T entity, bool all = false)
        {
            return await db.UpdateAsync<T>(entity, all);
        }

        public async Task<int> UpdateAsync(IEnumerable<T> entities, bool all = false)
        {
            return await db.UpdateAsync<T>(entities, all);
        }

        public async Task<int> SpUpdateAsync(T entity, bool all = false)
        {
            return await db.UpdateAsync<T>(entity, all);
        }

        public async Task<int> SpUpdateAsync(IEnumerable<T> entities, bool all = false)
        {
            return await db.UpdateAsync<T>(entities, all);
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
        public IEnumerable<T> RunProcedureList(string strSql, SugarParameter[] parameters)
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
        public T FindEntity(object keyValue)
        {
            return db.FindEntity<T>(keyValue);
        }
        public T FindEntity(Expression<Func<T, bool>> condition)
        {
            return db.FindEntity<T>(condition);
        }
        public T FindEntity(Expression<Func<T, bool>> condition, string orderby)
        {
            return db.FindEntity<T>(condition, orderby);
        }
        public T FindEntity(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type)
        {
            return db.FindEntity<T>(condition, orderby, type);
        }

        public async Task<T> FindEntityAsync(object keyValue)
        {
            return await db.FindEntityAsync<T>(keyValue);
        }

        public async Task<T> FindEntityAsync(Expression<Func<T, bool>> condition)
        {
            return await db.FindEntityAsync<T>(condition);
        }

        public async Task<T> FindEntityAsync(Expression<Func<T, bool>> condition, string orderby)
        {
            return await db.FindEntityAsync<T>(condition, orderby);
        }

        public async Task<T> FindEntityAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type)
        {
            return await db.FindEntityAsync<T>(condition, orderby, type);
        }
        #endregion

        #region 获取长度
        public int FindCount<T>() where T : class, new()
        {
            return db.FindCount<T>();
        }
        public int FindCount(Expression<Func<T, bool>> condition)
        {
            return db.FindCount<T>(condition);
        }
        public int FindCount(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split)
        {
            return db.FindCount<T>(condition, split);
        }

        public async Task<int> FindCountAsync()
        {
            return await db.FindCountAsync<T>();
        }

        public async Task<int> FindCountAsync(Expression<Func<T, bool>> condition)
        {
            return await db.FindCountAsync<T>(condition);
        }

        public async Task<int> FindCountAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split)
        {
            return await db.FindCountAsync<T>(condition, split);
        }
        #endregion

        #region 延迟查询
        public ISugarQueryable<T> Queryable()
        {
            return db.Queryable<T>();
        }
        #endregion

        #region 实体集合
        public IEnumerable<T> FindList()
        {
            return db.FindList<T>();
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition)
        {
            return db.FindList<T>(condition);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, string orderby)
        {
            return db.FindList<T>(condition, orderby);
        }
        public IEnumerable<T> FindList(Expression<Func<T, object>> orderby, OrderByType type)
        {
            return db.FindList<T>(orderby, type);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type)
        {
            return db.FindList<T>(condition, orderby, type);
        }

        public IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split)
        {
            return db.FindList<T>(split);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split)
        {
            return db.FindList<T>(condition, split);
        }
        public IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type)
        {
            return db.FindList<T>(split, orderby, type);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby)
        {
            return db.FindList<T>(condition, split, orderby);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type)
        {
            return db.FindList<T>(condition, split, orderby, type);
        }

        public async Task<IEnumerable<T>> FindListAsync()
        {
            return await db.FindListAsync<T>();
        }

        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, object>> orderby, OrderByType type)
        {
            return await db.FindListAsync<T>(orderby, type);
        }

        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition)
        {
            return await db.FindListAsync<T>(condition);
        }

        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, string orderby)
        {
            return await db.FindListAsync<T>(condition, orderby);
        }

        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type)
        {
            return await db.FindListAsync<T>(condition, orderby, type);
        }

        public async Task<IEnumerable<T>> FindListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split)
        {
            return await db.FindListAsync<T>(split);
        }

        public async Task<IEnumerable<T>> FindListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type)
        {
            return await db.FindListAsync<T>(split, orderby,type);
        }

        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split)
        {
            return await db.FindListAsync<T>(condition, split);
        }

        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby)
        {
            return await db.FindListAsync<T>(condition, split, orderby);
        }

        public async Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type)
        {
            return await db.FindListAsync<T>(condition, split, orderby, type);
        }
        #endregion

        #region 实体分页
        public IEnumerable<T> FindList(Pagination pagination)
        {
            int total = pagination.Records;
            var data = db.FindList<T>(pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            int total = pagination.Records;
            var data = db.FindList<T>(condition, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(orderby, type, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(orderby, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(condition, orderby, type, pageIndex, pageSize, out total);
        }

        public IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination)
        {
            int total = pagination.Records;
            var data = db.FindList<T>(split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination)
        {
            int total = pagination.Records;
            var data = db.FindList<T>(condition, split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(split, orderby, type, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(condition, split, orderby, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(condition, split, orderby, type, pageIndex, pageSize, out total);
        }

        #endregion

        #region sql获取实体集合
        public IEnumerable<T> FindList(string strSql)
        {
            return db.FindList<T>(strSql);
        }

        public IEnumerable<T> FindList(string strSql, SugarParameter[] parameters)
        {
            return db.FindList<T>(strSql, parameters);
        }

        public async Task<IEnumerable<T>> FindListAsync(string strSql)
        {
            return await db.FindListAsync<T>(strSql);
        }

        public async Task<IEnumerable<T>> FindListAsync(string strSql, SugarParameter[] parameters)
        {
            return await db.FindListAsync<T>(strSql, parameters);
        }
        #endregion

        #region 实体sql分页
        public IEnumerable<T> FindList(string orderField, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(orderField, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList(string strSql, Pagination pagination)
        {
            int total = pagination.Records;
            var data = db.FindList<T>(strSql, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList(string strSql, SugarParameter[] parameters, Pagination pagination)
        {
            int total = pagination.Records;
            var data = db.FindList<T>(strSql, parameters, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize, out total);
            pagination.Records = total;
            return data;
        }
        public IEnumerable<T> FindList(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(strSql, orderField, isAsc, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize, out int total)
        {
            return db.FindList<T>(strSql, parameters, orderField, isAsc, pageIndex, pageSize, out total);
        }
        public IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize, out int total)
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
        public IPagedList<T> ToPagedList(Pagination pagination)
        {
            var result = db.ToPagedList<T>(pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            var result = db.ToPagedList<T>(condition, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize)
        {
            var result = db.ToPagedList<T>(orderby, type, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize)
        {
            var result = db.ToPagedList<T>(orderby, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize)
        {
            var result = db.ToPagedList<T>(condition, orderby, type, pageIndex, pageSize);
            return result;
        }

        public IPagedList<T> ToPagedList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination)
        {
            var result = db.ToPagedList<T>(split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination)
        {
            var result = db.ToPagedList<T>(condition, split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize)
        {
            var result = db.ToPagedList<T>(split, orderby, type, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize)
        {
            var result = db.ToPagedList<T>(condition, split, orderby, pageIndex, pageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize)
        {
            var result = db.ToPagedList<T>(condition, split, orderby, type, pageIndex, pageSize);
            return result;
        }

        public IPagedList<T> ToPagedList(string orderField, int pageIndex, int pageSize)
        {
            return db.ToPagedList<T>(orderField, pageIndex, pageSize);
        }
        public IPagedList<T> ToPagedList(string strSql, Pagination pagination)
        {
            var result = db.ToPagedList<T>(strSql, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(string strSql, SugarParameter[] parameters, Pagination pagination)
        {
            var result = db.ToPagedList<T>(strSql, parameters, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public IPagedList<T> ToPagedList(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize)
        {
            return db.ToPagedList<T>(strSql, orderField, isAsc, pageIndex, pageSize);
        }
        public IPagedList<T> ToPagedList(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize)
        {
            return db.ToPagedList<T>(strSql, parameters, orderField, isAsc, pageIndex, pageSize);
        }
        public IPagedList<T> ToPagedList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize)
        {
            return db.ToPagedList<T>(split, orderField, pageIndex, pageSize);
        }


        public async Task<IPagedList<T>> ToPagedListAsync(Pagination pagination)
        {
            var result = await db.ToPagedListAsync<T>(pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            var result = await db.ToPagedListAsync<T>(condition, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize)
        {
            var result = await db.ToPagedListAsync<T>(orderby, type, pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize)
        {
            var result = await db.ToPagedListAsync<T>(orderby, pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize)
        {
            var result = await db.ToPagedListAsync<T>(condition, orderby, type, pageIndex, pageSize);
            return result;
        }

        public async Task<IPagedList<T>> ToPagedListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination)
        {
            var result = await db.ToPagedListAsync<T>(split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination)
        {
            var result = await db.ToPagedListAsync<T>(condition, split, pagination.OrderFiled + " " + pagination.OrderByType, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize)
        {
            var result = await db.ToPagedListAsync<T>(split, orderby, type, pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize)
        {
            var result = await db.ToPagedListAsync<T>(condition, split, orderby, pageIndex, pageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize)
        {
            var result = await db.ToPagedListAsync<T>(condition, split, orderby, type, pageIndex, pageSize);
            return result;
        }

        public async Task<IPagedList<T>> ToPagedListAsync(string orderField, int pageIndex, int pageSize)
        {
            return await db.ToPagedListAsync<T>(orderField, pageIndex, pageSize);
        }
        public async Task<IPagedList<T>> ToPagedListAsync(string strSql, Pagination pagination)
        {
            var result = await db.ToPagedListAsync<T>(strSql, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(string strSql, SugarParameter[] parameters, Pagination pagination)
        {
            var result = await db.ToPagedListAsync<T>(strSql, parameters, pagination.OrderFiled, pagination.OrderByType.ToUpper() == "ASC" ? true : false, pagination.PageIndex, pagination.PageSize);
            return result;
        }
        public async Task<IPagedList<T>> ToPagedListAsync(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize)
        {
            return await db.ToPagedListAsync<T>(strSql, orderField, isAsc, pageIndex, pageSize);
        }
        public async Task<IPagedList<T>> ToPagedListAsync(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize)
        {
            return await db.ToPagedListAsync<T>(strSql, parameters, orderField, isAsc, pageIndex, pageSize);
        }
        public async Task<IPagedList<T>> ToPagedListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize)
        {
            return await db.ToPagedListAsync<T>(split, orderField, pageIndex, pageSize);
        }
        #endregion
    }
}
