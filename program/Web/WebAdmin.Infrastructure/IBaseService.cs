namespace WebAdmin.Infrastructure;

/// <summary>
/// 泛型基类接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseService<T> where T : class, new()
{
    #region 获取数据   
    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <returns></returns>
    T GetEntity(string keyValue);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ISugarQueryable<T> Queryable();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> GetList();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="split"></param>
    /// <returns></returns>
    IEnumerable<T> GetList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split);
    #endregion

    #region 提交数据
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="keyValue">主键值</param>
    void RemoveForm(string keyValue);
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="keyValue">主键值数组</param>
    void RemoveForm(string[] keyValue);

    /// <summary>
    /// 保存表单(新增、修改)
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <param name="entity">实体对象</param>
    /// <param name="all">是否更新所有字段</param>
    /// <returns></returns>
    void MvcSaveForm(string keyValue, T entity, bool all = false);
    /// <summary>
    /// 保存表单(新增、修改)
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <param name="entity">实体对象</param>
    /// <param name="all">是否更新所有字段</param>
    /// <returns></returns>
    void ApiSaveForm(string keyValue, T entity, bool all = false);
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="keyValue">主键值</param>
    void SpRemoveForm(string keyValue);
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="keyValue">主键值数组</param>
    void SpRemoveForm(string[] keyValue);
    /// <summary>
    /// 保存表单(新增、修改)
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <param name="entity">实体对象</param>
    /// <param name="all">是否更新所有字段</param>
    /// <returns></returns>
    void MvcSpSaveForm(string keyValue, T entity, bool all = false);
    /// <summary>
    /// 保存表单(新增、修改)
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <param name="entity">实体对象</param>
    /// <param name="all">是否更新所有字段</param>
    /// <returns></returns>
    void ApiSpSaveForm(string keyValue, T entity, bool all = false);
    #endregion
}