namespace WebAdmin.Service;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseService<T> : IBaseService<T> where T : class, new()
{
    private readonly BaseRepository<T> _repository;
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="repository"></param>
    public BaseService(BaseRepository<T> repository)
    {
        _repository = repository;
    }
    #region 获取数据
    /// <summary>
    /// 延迟查询
    /// </summary>
    /// <returns></returns>
    public ISugarQueryable<T> Queryable()
    {
        return _repository.Queryable();
    }
    /// <summary>
    /// 规则列表
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> GetList()
    {
        return _repository.GetList();
    }
    /// <summary>
    /// 规则列表
    /// </summary>
    /// <param name="split"></param>
    /// <returns></returns>
    public IEnumerable<T> GetList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split)
    {
        return _repository.GetList(split);
    }
    /// <summary>
    /// 规则实体
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <returns></returns>
    public T GetEntity(string keyValue)
    {
        return _repository.GetEntity(keyValue);
    }
    #endregion

    #region 提交数据
    /// <summary>
    /// 删除规则
    /// </summary>
    /// <param name="keyValue">主键</param>
    public void RemoveForm(string keyValue)
    {
        _repository.RemoveForm(keyValue);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyValue"></param>
    public void RemoveForm(string[] keyValue)
    {
        _repository.RemoveForm(keyValue);
    }
    /// <summary>
    /// 保存规则表单(新增、修改)
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <param name="Entity">规则实体</param>
    /// <param name="Newent">是否开启post请求表单</param>
    /// <param name="Vali">是否开启表单验证</param>
    /// <param name="all">是否更新所有字段</param>
    /// <returns></returns>
    public void MvcSaveForm(string keyValue, T Entity, bool Newent = false, bool Vali = false, bool all = false)
    {
        try
        {
            if (Newent && !string.IsNullOrWhiteSpace(keyValue))
            {
                var entt = _repository.GetEntity(keyValue);
                if (entt != null)
                {
                    PropertyInfo[] pArray1 = entt.GetType().GetProperties();
                    foreach (var p in pArray1)
                    {
                        if (AppHttpContext.Current.Request.Form.Keys.Count(t => t.ToLower() == p.Name.ToLower()) > 0)
                            p.SetValue(entt, p.GetValue(Entity, null));
                    }
                    if (Vali)
                    {
                        var error = entt.Validate();
                        if (error != "")
                            throw new BusinessException(error);
                    }
                    _repository.MvcSaveForm(keyValue, entt, true);
                }
                else
                    throw new BusinessException("数据不存在");
            }
            else
            {
                if (Vali)
                {
                    var error = Entity.Validate();
                    if (error != "")
                        throw new Exception(error);
                }
                _repository.MvcSaveForm(keyValue, Entity, all);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// 保存规则表单(新增、修改)
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <param name="Entity">规则实体</param>
    /// <param name="Newent">是否开启post请求表单</param>
    /// <param name="Vali">是否开启表单验证</param>
    /// <param name="all">是否更新所有字段</param>
    /// <returns></returns>
    public void ApiSaveForm(string keyValue, T Entity, bool Newent = false, bool Vali = false, bool all = false)
    {
        try
        {
            if (Newent && !string.IsNullOrWhiteSpace(keyValue))
            {
                var entt = _repository.GetEntity(keyValue);
                if (entt != null)
                {
                    PropertyInfo[] pArray1 = entt.GetType().GetProperties();
                    foreach (var p in pArray1)
                    {
                        if (AppHttpContext.Current.Request.Form.Keys.Count(t => t.ToLower() == p.Name.ToLower()) > 0)
                            p.SetValue(entt, p.GetValue(Entity, null));
                    }
                    if (Vali)
                    {
                        var error = entt.Validate();
                        if (error != "")
                            throw new BusinessException(error);
                    }
                    _repository.ApiSaveForm(keyValue, entt, true);
                }
                else
                    throw new BusinessException("数据不存在");
            }
            else
            {
                if (Vali)
                {
                    var error = Entity.Validate();
                    if (error != "")
                        throw new Exception(error);
                }
                _repository.ApiSaveForm(keyValue, Entity, all);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// 删除规则
    /// </summary>
    /// <param name="keyValue">主键</param>
    public void SpRemoveForm(string keyValue)
    {
        try
        {
            _repository.SpRemoveForm(keyValue);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// 保存规则表单(新增、修改)
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <param name="Entity">规则实体</param>
    /// <param name="Newent">是否开启post请求表单</param>
    /// <param name="Vali">是否开启表单验证</param>
    /// <param name="all">是否更新所有字段</param>
    /// <returns></returns>
    public void MvcSpSaveForm(string keyValue, T Entity, bool Newent = false, bool Vali = false, bool all = false)
    {
        try
        {
            if (Newent && !string.IsNullOrWhiteSpace(keyValue))
            {
                PropertyInfo[] pArray1 = Entity.GetType().GetProperties();
                foreach (var p in pArray1)
                {
                    if (AppHttpContext.Current.Request.Form.Keys.Count(t => t.ToLower() == p.Name.ToLower()) > 0)
                    {
                        try
                        {
                            p.SetValue(Entity, AppHttpContext.Current.Request.Form[p.Name].ToString() ?? "");
                        }
                        catch(Exception ex)
                        {
                        }
                    }
                }
                if (Vali)
                {
                    var error = Entity.Validate();
                    if (error != "")
                        throw new BusinessException(error);
                }
                _repository.MvcSpSaveForm(keyValue, Entity, all);
            }
            else
            {
                if (Vali)
                {
                    var error = Entity.Validate();
                    if (error != "")
                        throw new BusinessException(error);
                }
                _repository.MvcSpSaveForm(keyValue, Entity, all);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// 保存规则表单(新增、修改)
    /// </summary>
    /// <param name="keyValue">主键值</param>
    /// <param name="Entity">规则实体</param>
    /// <param name="Newent">是否开启post请求表单</param>
    /// <param name="Vali">是否开启表单验证</param>
    /// <param name="all">是否更新所有字段</param>
    /// <returns></returns>
    public void ApiSpSaveForm(string keyValue, T Entity, bool Newent = false, bool Vali = false, bool all = false)
    {
        try
        {
            if (Newent && !string.IsNullOrWhiteSpace(keyValue))
            {
                PropertyInfo[] pArray1 = Entity.GetType().GetProperties();
                foreach (var p in pArray1)
                {
                    if (AppHttpContext.Current.Request.Form.Keys.Count(t => t.ToLower() == p.Name.ToLower()) > 0)
                    {
                        try
                        {
                            p.SetValue(Entity, AppHttpContext.Current.Request.Form[p.Name].ToString() ?? "");
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                if (Vali)
                {
                    var error = Entity.Validate();
                    if (error != "")
                        throw new BusinessException(error);
                }
                _repository.ApiSpSaveForm(keyValue, Entity, all);
            }
            else
            {
                if (Vali)
                {
                    var error = Entity.Validate();
                    if (error != "")
                        throw new BusinessException(error);
                }
                _repository.ApiSpSaveForm(keyValue, Entity, all);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyValue"></param>
    /// <param name="entity"></param>
    /// <param name="all"></param>
    public void MvcSaveForm(string keyValue, T entity, bool all = false)
    {
        _repository.MvcSaveForm(keyValue, entity, all);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyValue"></param>
    /// <param name="entity"></param>
    /// <param name="all"></param>
    public void ApiSaveForm(string keyValue, T entity, bool all = false)
    {
        _repository.ApiSaveForm(keyValue, entity, all);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyValue"></param>
    public void SpRemoveForm(string[] keyValue)
    {
        _repository.SpRemoveForm(keyValue);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyValue"></param>
    /// <param name="entity"></param>
    /// <param name="all"></param>
    public void MvcSpSaveForm(string keyValue, T entity, bool all = false)
    {
        _repository.MvcSpSaveForm(keyValue, entity, all);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyValue"></param>
    /// <param name="entity"></param>
    /// <param name="all"></param>
    public void ApiSpSaveForm(string keyValue, T entity, bool all = false)
    {
        _repository.ApiSpSaveForm(keyValue, entity, all);
    }
    #endregion
}