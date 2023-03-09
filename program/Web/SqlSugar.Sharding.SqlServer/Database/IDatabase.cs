using SqlSugar;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using WebAdmin.Domain.EntityBase;
using X.PagedList;

namespace SqlSugar.Sharding.SqlServer
{
    public interface IDatabase
    {
        SqlSugarClient SqlSugarClient();
        IDatabase Db();
        void RemoveCache(string Key);

        #region 事务
        IDatabase BeginTran();
        void Commit();
        void Rollback();

        Task<IDatabase> BeginTranAsync();
        Task CommitAsync();
        Task RollbackAsync();
        #endregion

        #region 插入
        int Insert<T>(T entity) where T : class, new();
        int Insert<T>(IEnumerable<T> entities) where T : class, new();

        int SpInsert<T>(T entity) where T : class, new();
        int SpInsert<T>(IEnumerable<T> entities) where T : class, new();

        int TableSave(DataTable dt, string tableName, string pkName);

        Task<int> InsertAsync<T>(T entity) where T : class, new();
        Task<int> InsertAsync<T>(IEnumerable<T> entities) where T : class, new();

        Task<int> SpInsertAsync<T>(T entity) where T : class, new();
        Task<int> SpInsertAsync<T>(IEnumerable<T> entities) where T : class, new();

        Task<int> TableSaveAsync(DataTable dt, string tableName, string pkName);
        #endregion

        #region 删除
        int Delete<T>() where T : class, new();
        int Delete<T>(string keyValue) where T : class, new();
        int Delete<T>(string[] keyValue) where T : class, new();
        int Delete<T>(T entity) where T : class, new();
        int Delete<T>(IEnumerable<T> entities) where T : class, new();
        int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new();

        int SpDelete<T>() where T : class, new();
        int SpDelete<T>(string keyValue) where T : class, new();
        int SpDelete<T>(string[] keyValue) where T : class, new();
        int SpDelete<T>(T entity) where T : class, new();
        int SpDelete<T>(IEnumerable<T> entities) where T : class, new();
        int SpDelete<T>(Expression<Func<T, bool>> condition) where T : class, new();


        Task<int> DeleteAsync<T>() where T : class, new();
        Task<int> DeleteAsync<T>(string keyValue) where T : class, new();
        Task<int> DeleteAsync<T>(string[] keyValue) where T : class, new();
        Task<int> DeleteAsync<T>(T entity) where T : class, new();
        Task<int> DeleteAsync<T>(IEnumerable<T> entities) where T : class, new();
        Task<int> DeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new();

        Task<int> SpDeleteAsync<T>() where T : class, new();
        Task<int> SpDeleteAsync<T>(string keyValue) where T : class, new();
        Task<int> SpDeleteAsync<T>(string[] keyValue) where T : class, new();
        Task<int> SpDeleteAsync<T>(T entity) where T : class, new();
        Task<int> SpDeleteAsync<T>(IEnumerable<T> entities) where T : class, new();
        Task<int> SpDeleteAsync<T>(Expression<Func<T, bool>> condition) where T : class, new();
        #endregion

        #region 更新
        int Update<T>(T entity, bool all = false) where T : class, new();
        int Update<T>(IEnumerable<T> entities, bool all = false) where T : class, new();


        int SpUpdate<T>(T entity, bool all = false) where T : class, new();
        int SpUpdate<T>(IEnumerable<T> entities, bool all = false) where T : class, new();

        Task<int> UpdateAsync<T>(T entity, bool all = false) where T : class, new();
        Task<int> UpdateAsync<T>(IEnumerable<T> entities, bool all = false) where T : class, new();

        Task<int> SpUpdateAsync<T>(T entity, bool all = false) where T : class, new();
        Task<int> SpUpdateAsync<T>(IEnumerable<T> entities, bool all = false) where T : class, new();
        #endregion

        #region 存储过程
        DataSet RunProcedureDataSet(string strSql, SugarParameter[] parameters);
        int RunProcedure(string strSql, SugarParameter[] parameters);
        IEnumerable<T> RunProcedureList<T>(string strSql, SugarParameter[] parameters);

        Task<DataSet> RunProcedureDataSetAsync(string strSql, SugarParameter[] parameters);
        Task<int> RunProcedureAsync(string strSql, SugarParameter[] parameters);
        Task<IEnumerable<T>> RunProcedureListAsync<T>(string strSql, SugarParameter[] parameters);
        #endregion

        #region Ado.Net
        DataSet FindDataSet(string strSql, SugarParameter[] parameters);
        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, SugarParameter[] parameters);
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize, out int total);
        DataTable FindTable(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize, out int total);


        Task<DataSet> FindDataSetAsync(string strSql, SugarParameter[] parameters);
        Task<DataTable> FindTableAsync(string strSql);
        Task<DataTable> FindTableAsync(string strSql, SugarParameter[] parameters);
        #endregion

        #region 获取实体
        T FindEntity<T>(object KeyValue) where T : class;
        T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new();
        T FindEntity<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new();
        T FindEntity<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();

        Task<T> FindEntityAsync<T>(object KeyValue) where T : class;
        Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition) where T : class, new();
        Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new();
        Task<T> FindEntityAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();
        #endregion

        #region 获取长度
        int FindCount<T>() where T : class, new();
        int FindCount<T>(Expression<Func<T, bool>> condition) where T : class, new();
        int FindCount<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new();

        Task<int> FindCountAsync<T>() where T : class, new();
        Task<int> FindCountAsync<T>(Expression<Func<T, bool>> condition) where T : class, new();
        Task<int> FindCountAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new();
        #endregion

        #region 延迟查询
        ISugarQueryable<T> Queryable<T>() where T : class, new();
        #endregion

        #region 实体集合
        IEnumerable<T> FindList<T>() where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();

        IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new();
        IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();



        Task<IEnumerable<T>> FindListAsync<T>() where T : class, new();
        Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();
        Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition) where T : class, new();
        Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, string orderby) where T : class, new();
        Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();

        Task<IEnumerable<T>> FindListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new();
        Task<IEnumerable<T>> FindListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();
        Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split) where T : class, new();
        Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby) where T : class, new();
        Task<IEnumerable<T>> FindListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type) where T : class, new();
        #endregion

        #region 实体分页
        IEnumerable<T> FindList<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class;
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize, out int total) where T : class;
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class;

        IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class;
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize, out int total) where T : class;
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize, out int total) where T : class;
        #endregion

        #region sql获取实体集合
        IEnumerable<T> FindList<T>(string strSql) where T : class;
        IEnumerable<T> FindList<T>(string strSql, SugarParameter[] parameters) where T : class;

        Task<IEnumerable<T>> FindListAsync<T>(string strSql) where T : class;
        Task<IEnumerable<T>> FindListAsync<T>(string strSql, SugarParameter[] parameters) where T : class;
        #endregion

        #region 实体sql分页

        IEnumerable<T> FindList<T>(string orderField, int pageIndex, int pageSize, out int total) where T : class;
        IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize, out int total) where T : class, new();
        IEnumerable<T> FindList<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize, out int total) where T : class, new();
        IEnumerable<T> FindList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize, out int total) where T : class;

        #endregion

        #region 实体生成同步
        bool CreateEntity(string TableName, string Path);
        bool CreateTable(string EntityName, bool bak = false);
        #endregion

        #region X.PagedList分页组件
        IPagedList<T> ToPagedList<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class;
        IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize) where T : class;
        IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class;
        IPagedList<T> ToPagedList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class;
        IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize) where T : class;
        IPagedList<T> ToPagedList<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class;

        IPagedList<T> ToPagedList<T>(string orderField, int pageIndex, int pageSize) where T : class;
        IPagedList<T> ToPagedList<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new();
        IPagedList<T> ToPagedList<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new();
        IPagedList<T> ToPagedList<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize) where T : class;

        Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class;
        Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, string orderby, int pageIndex, int pageSize) where T : class;
        Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class;
        Task<IPagedList<T>> ToPagedListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class;
        Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderby, int pageIndex, int pageSize) where T : class;
        Task<IPagedList<T>> ToPagedListAsync<T>(Expression<Func<T, bool>> condition, Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, Expression<Func<T, object>> orderby, OrderByType type, int pageIndex, int pageSize) where T : class;

        Task<IPagedList<T>> ToPagedListAsync<T>(string orderField, int pageIndex, int pageSize) where T : class;
        Task<IPagedList<T>> ToPagedListAsync<T>(string strSql, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new();
        Task<IPagedList<T>> ToPagedListAsync<T>(string strSql, SugarParameter[] parameters, string orderField, bool isAsc, int pageIndex, int pageSize) where T : class, new();
        Task<IPagedList<T>> ToPagedListAsync<T>(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split, string orderField, int pageIndex, int pageSize) where T : class;
        #endregion
    }
}
