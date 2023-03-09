using WebAdmin.Framework.Extentions.Exceptions;
using WebAdmin.Framework.Extentions;
using WebAdmin.Framework.Providers;
using X.PagedList;

namespace WebAdmin.Infrastructure.Base;

/// <summary>
/// 用户管理
/// </summary>
public interface IUserService : IBaseService<UserEntity>
{
    #region 获取数据
    /// <summary>
    /// 根据token查询用户
    /// </summary>
    /// <param name="token">token</param>
    /// <returns></returns>
    UserEntity GetEntityByToken(string token);
    /// <summary>
    /// 根据条件查询用户列表
    /// </summary>
    /// <param name="queryJson">查询参数</param>
    /// <returns></returns>
    IEnumerable<UserEntity> GetList(string queryJson);
    /// <summary>
    /// 用户列表
    /// </summary>
    /// <param name="pagination">分页</param>
    /// <returns></returns>
    IPagedList<UserEntity> GetPagedList(Pagination pagination);
    /// <summary>
    /// 根据条件查询用户分页列表
    /// </summary>
    /// <param name="pagination">分页参数</param>
    /// <returns></returns>
    Task<IPagedList<UserEntity>> GetPagedListAsync(Pagination pagination);
    #endregion

    #region 验证数据
    /// <summary>
    /// 账户不能重复
    /// </summary>
    /// <param name="account">账户值</param>
    /// <param name="keyValue">主键</param>
    /// <returns></returns>
    bool ExistAccount(string account, string keyValue);
    /// <summary>
    /// 登录验证
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    UserEntity CheckLogin(string username, string password);
    #endregion

    #region 保存数据

    #endregion

}
