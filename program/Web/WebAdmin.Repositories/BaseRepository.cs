namespace WebAdmin.Repositories;

/// <summary>
/// 泛型基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseRepository<T> : RepositoryFactory<T> where T : class, new()
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual ISugarQueryable<T> Queryable()
    {
        return this.BaseRepository().Queryable();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerable<T> GetList()
    {
        return this.BaseRepository().FindList();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual T GetEntity(string keyValue)
    {
        return this.BaseRepository().FindEntity(keyValue);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual void RemoveForm(string keyValue)
    {
        this.BaseRepository().Delete(keyValue);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual void SpRemoveForm(string keyValue)
    {
        this.BaseRepository().SpDelete(keyValue);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual void RemoveForm(string[] keyValue)
    {
        this.BaseRepository().Delete(keyValue);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual void SpRemoveForm(string[] keyValue)
    {
        this.BaseRepository().SpDelete(keyValue);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual void MvcSaveForm(string keyValue, T entity, bool all = false)
    {
        dynamic ent = entity;
        if (!string.IsNullOrEmpty(keyValue))
        {
            ent.MvcModify(keyValue);
            this.BaseRepository().Update(entity, all);
        }
        else
        {
            ent.MvcCreate();
            this.BaseRepository().Insert(entity);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual void ApiSaveForm(string keyValue, T entity, bool all = false)
    {
        dynamic ent = entity;
        if (!string.IsNullOrEmpty(keyValue))
        {
            ent.ApiModify(keyValue);
            this.BaseRepository().Update(entity, all);
        }
        else
        {
            ent.ApiCreate();
            this.BaseRepository().Insert(entity);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual void MvcSpSaveForm(string keyValue, T entity, bool all = false)
    {
        dynamic ent = entity;
        if (!string.IsNullOrEmpty(keyValue))
        {
            ent.MvcModify(keyValue);
            this.BaseRepository().SpUpdate(entity, all);
        }
        else
        {
            ent.MvcCreate();
            this.BaseRepository().SpInsert(entity);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyValue"></param>
    /// <param name="entity"></param>
    /// <param name="all"></param>
    public virtual void ApiSpSaveForm(string keyValue, T entity, bool all = false)
    {
        dynamic ent = entity;
        if (!string.IsNullOrEmpty(keyValue))
        {
            ent.ApiModify(keyValue);
            this.BaseRepository().SpUpdate(entity, all);
        }
        else
        {
            ent.ApiCreate();
            this.BaseRepository().SpInsert(entity);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerable<T> GetList(Func<IEnumerable<SplitTableInfo>, IEnumerable<SplitTableInfo>> split)
    {
        return this.BaseRepository().FindList(split);
    }
}