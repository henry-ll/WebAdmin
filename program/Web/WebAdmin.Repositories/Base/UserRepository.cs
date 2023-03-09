namespace WebAdmin.Repositories.Base;

/// <summary>
/// 用户管理
/// </summary>
public class UserRepository : BaseRepository<UserEntity>
{
    #region 获取数据
    /// <summary>
    /// 根据token查询用户
    /// </summary>
    /// <param name="token">token</param>
    /// <returns></returns>
    public UserEntity GetEntityByToken(string token)
    {
        var expression = LinqExtension.True<UserEntity>();
        expression = expression.And(t => t.JwtToKen == token);
        return this.BaseRepository().FindEntity(expression);
    }
    /// <summary>
    /// 根据条件查询用户列表
    /// </summary>
    /// <param name="queryJson">查询参数</param>
    /// <returns></returns>
    public IEnumerable<UserEntity> GetList(string queryJson)
    {
        var expression = LinqExtension.True<UserEntity>();
        var queryParam = queryJson.ToJObject();
        //查询条件
        if (!queryParam["keyword"].IsEmpty())
        {
            string keyword = queryParam["keyword"].ToString();
            expression = expression.And(t => t.Account.Contains(keyword) || t.RealName.Contains(keyword));
        }
        return this.BaseRepository().FindList(expression);
    }
    /// <summary>
    /// 用户列表
    /// </summary>
    /// <param name="pagination">分页</param>
    /// <returns></returns>
    public IPagedList<UserEntity> GetPagedList(Pagination pagination)
    {
        var expression = LinqExtension.True<UserEntity>();
        //查询条件
        if (!pagination.Categorykey.IsEmpty())
        {
            string categorykey = pagination.Categorykey;
            expression = expression.And(t => t.Account.Contains(categorykey) || t.RealName.Contains(categorykey));
        }
        expression = expression.And(t => t.Id != "System");
        return this.BaseRepository().ToPagedList(expression, pagination);
    }

    /// <summary>
    /// 用户列表
    /// </summary>
    /// <param name="pagination">分页</param>
    /// <returns></returns>
    public async  Task<IPagedList<UserEntity>> GetPagedListAsync(Pagination pagination)
    {
        var expression = LinqExtension.True<UserEntity>();
        //查询条件
        if (!pagination.Categorykey.IsEmpty())
        {
            string categorykey = pagination.Categorykey;
            expression = expression.And(t => t.Account.Contains(categorykey) || t.RealName.Contains(categorykey));
        }
        expression = expression.And(t => t.Id != "System");
        return await this.BaseRepository().ToPagedListAsync(expression, pagination);
    }
    #endregion

    #region 验证数据
    /// <summary>
    /// 账户不能重复
    /// </summary>
    /// <param name="account">账户值</param>
    /// <param name="keyValue">主键</param>
    /// <returns></returns>
    public bool ExistAccount(string account, string keyValue)
    {
        var expression = LinqExtension.True<UserEntity>();
        expression = expression.And(t => t.Account == account);
        if (!string.IsNullOrWhiteSpace(keyValue))
            expression = expression.And(t => t.Id != keyValue);
        return this.BaseRepository().FindCount(expression) == 0 ? true : false;
    }
    /// <summary>
    /// 登录验证
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns></returns>
    public UserEntity CheckLogin(string username)
    {
        var expression = LinqExtension.True<UserEntity>();
        expression = expression.And(t => t.Account == username);
        return this.BaseRepository().FindEntity(expression);
    }
    #endregion

    #region 保存数据

    #endregion
}
