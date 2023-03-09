using SqlSugar;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using WebAdmin.Domain.EntityBase;
using X.PagedList;

namespace SqlSugar.Sharding.SqlServer
{
    /// <summary>
    /// 定义仓储模型中的数据标准操作接口
    /// </summary>
    /// <typeparam name="T">动态实体类型</typeparam>
    public interface IRepository<T>
    {
        SqlSugarClient SqlSugarClient();
        IRepository<T> Db();
        void RemoveCache(string key);

        #region 事务
        IRepository<T> BeginTrans();

        void Commit();

        void Rollback();


        Task<IRepository<T>> BeginTranAsync();
        Task CommitAsync();
        Task RollbackAsync();
        #endregion

        #region 插入
        int Insert(T entity);
        int Insert(IEnumerable<T> entities) ;
        int SpInsert(T entity);
        int SpInsert(IEnumerable<T> entities);

        int TableSave(DataTable dt, string TableName, string PkName);

        Task<int> InsertAsync(T entity);
        Task<int> InsertAsync(IEnumerable<T> entities);

        Task<int> SpInsertAsync(T entity);
        Task<int> SpInsertAsync(IEnumerable<T> entities);

        Task<int> TableSaveAsync(DataTable dt, string tableName, string pkName);
        #endregion

        #region 删除
        int Delete() ;
        int Delete(string keyValue) ;
        int Delete(string[] keyValue) ;
        int Delete(T entity) ;
        int Delete(IEnumerable<T> entities) ;
        int Delete(Expression<Func<T, bool>> condition) ;

        int SpDelete();
        int SpDelete(string keyValue);
        int SpDelete(string[] keyValue);
        int SpDelete(T entity);
        int SpDelete(IEnumerable<T> entities);
        int SpDelete(Expression<Func<T, bool>> condition);


        Task<int> DeleteAsync();
        Task<int> DeleteAsync(string keyValue);
        Task<int> DeleteAsync(string[] keyValue);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteAsync(IEnumerable<T> entities);
        Task<int> DeleteAsync(Expression<Func<T, bool>> condition);

        Task<int> SpDeleteAsync();
        Task<int> SpDeleteAsync(string keyValue);
        Task<int> SpDeleteAsync(string[] keyValue);
        Task<int> SpDeleteAsync(T entity);
        Task<int> SpDeleteAsync(IEnumerable<T> entities);
        Task<int> SpDeleteAsync(Expression<Func<T, bool>> condition);
        #endregion

        #region 更新
        int Update(T entity, bool all = false);
        int Update(IEnumerable<T> entities, bool all = false);

        int SpUpdate(T entity, bool all = false);
        int SpUpdate(IEnumerable<T> entities, bool all = false);


        Task<int> UpdateAsync(T entity, bool all = false);
        Task<int> UpdateAsync(IEnumerable<T> entities, bool all = false);

        Task<int> SpUpdateAsync(T entity, bool all = false);
        Task<int> SpUpdateAsync(IEnumerable<T> entities, bool all = false);
        #endregion

        #region 存储过程
        DataSet RunProcedureDataSet(string strSql, SugarParameter[] parameters);
        int RunProcedure(string strSql, SugarParameter[] parameters);
        IEnumerable<T> RunProcedureList(string strSql, SugarParameter[] parameters);

        Task<DataSet> RunProcedureDataSetAsync(string strSql, SugarParameter[] parameters);
        Task<int> RunProcedureAsync(string strSql, SugarParameter[] parameters);
        Task<IEnumerable<T>> RunProcedureListAsync<T>(string strSql, SugarParameter[] parameters);
        #endregion

        #region ADO.NET
        DataSet FindDataSet(string strSql, SugarParameter[] parameters);
        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, SugarParameter[] parameters);
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize, out int total);
        DataTable FindTable(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize, out int total);
        DataTable FindTable(string strSql, Pagination pagination);
        DataTable FindTable(string strSql, SugarParameter[] parameters, Pagination pagination);

        Task<DataSet> FindDataSetAsync(string strSql, SugarParameter[] parameters);
        Task<DataTable> FindTableAsync(string strSql);
        Task<DataTable> FindTableAsync(string strSql, SugarParameter[] parameters);
        #endregion

        #region 获取实体
        T FindEntity(object KeyValue) ;
        T FindEntity(Expression<Func<T, bool>> condition) ;
        T FindEntity(Expression<Func<T, bool>> condition, string orderby) ;
        T FindEntity(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) ;


        Task<T> FindEntityAsync(object KeyValue);
        Task<T> FindEntityAsync(Expression<Func<T, bool>> condition);
        Task<T> FindEntityAsync(Expression<Func<T, bool>> condition, string orderby);
        Task<T> FindEntityAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type);
        #endregion

        #region 获取长度
        int FindCount<T>() where T : class, new();
        int FindCount(Expression<Func<T, bool>> condition) ;
        int FindCount(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split);


        Task<int> FindCountAsync();
        Task<int> FindCountAsync(Expression<Func<T, bool>> condition);
        Task<int> FindCountAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split);
        #endregion

        #region 延迟查询
        ISugarQueryable<T> Queryable() ;
        #endregion

        #region 实体集合
        IEnumerable<T> FindList() ;
        IEnumerable<T> FindList(Expression<Func<T, object>> orderby, OrderByType type) ;
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition) ;
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, string orderby) ;
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) ;

        IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split);
        IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type);


        Task<IEnumerable<T>> FindListAsync();
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, object>> orderby, OrderByType type);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, string orderby);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type);

        Task<IEnumerable<T>> FindListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split);
        Task<IEnumerable<T>> FindListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby);
        Task<IEnumerable<T>> FindListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type);
        #endregion

        #region 实体分页
        IEnumerable<T> FindList(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) ;
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize, out int total) ;
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) ;
        IEnumerable<T> FindList(Pagination pagination);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination);

        IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize, out int total);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total);
        IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination);
        #endregion

        #region sql获取实体集合
        IEnumerable<T> FindList(string strSql) ;
        IEnumerable<T> FindList(string strSql, SugarParameter[] parameters) ;


        Task<IEnumerable<T>> FindListAsync(string strSql);
        Task<IEnumerable<T>> FindListAsync(string strSql, SugarParameter[] parameters);
        #endregion

        #region 实体sql分页
        IEnumerable<T> FindList(string orderField, int pageIndex, int pageSize, out int total) ;
        IEnumerable<T> FindList(string strSql, Pagination pagination) ;
        IEnumerable<T> FindList(string strSql, SugarParameter[] parameters, Pagination pagination);
        IEnumerable<T> FindList(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize, out int total) ;
        IEnumerable<T> FindList(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize, out int total) ;
        IEnumerable<T> FindList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize, out int total);
        #endregion

        #region 实体生成同步
        bool CreateEntity(string TableName, string Path);
        bool CreateTable(string EntityName, bool bak = false);
        #endregion

        #region X.PagedList分页组件
        IPagedList<T> ToPagedList(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(Pagination pagination);
        IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Pagination pagination);

        IPagedList<T> ToPagedList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination);
        IPagedList<T> ToPagedList(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination);

        IPagedList<T> ToPagedList(string orderField, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(string strSql, Pagination pagination);
        IPagedList<T> ToPagedList(string strSql, SugarParameter[] parameters, Pagination pagination);
        IPagedList<T> ToPagedList(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize);
        IPagedList<T> ToPagedList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize);


       Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(Pagination pagination);
        Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Pagination pagination);

        Task<IPagedList<T>> ToPagedListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination);
        Task<IPagedList<T>> ToPagedListAsync(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Pagination pagination);

        Task<IPagedList<T>> ToPagedListAsync(string orderField, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(string strSql, Pagination pagination);
        Task<IPagedList<T>> ToPagedListAsync(string strSql, SugarParameter[] parameters, Pagination pagination);
        Task<IPagedList<T>> ToPagedListAsync(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize);
        Task<IPagedList<T>> ToPagedListAsync(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize);
        #endregion
    }
}
