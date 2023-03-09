using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Domain.EntityBase;
using WebAdmin.Entity.Log;
using WebAdmin.Infrastructure.Log;
using WebAdmin.Repositories.Log;
using X.PagedList;

namespace WebAdmin.Service.Log;

/// <summary>
/// 登录日志
/// </summary>
public class LoginlogsService : BaseService<LoginlogsEntity>, ILoginlogsService
{
    /// <summary>
    /// 
    /// </summary>
    public LoginlogsRepository _loginlogsRepository;
    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="loginlogsRepository"></param>
    public LoginlogsService(LoginlogsRepository loginlogsRepository):base(loginlogsRepository)
    {
        _loginlogsRepository = loginlogsRepository;
    }
    /// <summary>
    /// 无参数构造函数
    /// </summary>
    public LoginlogsService() : base(new LoginlogsRepository())
    {
        _loginlogsRepository = new LoginlogsRepository();
    }
    /// <summary>
    /// 缓存key
    /// </summary>
    public string cacheKey = "loginlogsCache";

    #region 获取数据
    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="queryJson"></param>
    /// <returns></returns>
    public IEnumerable<LoginlogsEntity> GetList(string queryJson)
    {
        var result = _loginlogsRepository.GetList(queryJson);
        return result;
    }
    /// <summary>
    /// 分页 列表
    /// </summary>
    /// <param name="pagination"></param>
    /// <param name="queryJson"></param>
    /// <returns></returns>
    public IPagedList<LoginlogsEntity> GetPagedList(Pagination pagination, string queryJson)
    {
        var result = _loginlogsRepository.GetPagedList(pagination, queryJson);
        return result;
    }
    #endregion

    #region 验证数据

    #endregion

    #region 保存数据
    /// <summary>
    /// 保存Mvc端日志
    /// </summary>
    /// <param name="operateUserId"></param>
    /// <param name="operateAccount"></param>
    /// <param name="operateUserName"></param>
    /// <param name="IPAddress"></param>
    /// <param name="city"></param>
    /// <param name="host"></param>
    /// <param name="OS"></param>
    /// <param name="browser"></param>
    /// <param name="userAgent"></param>
    /// <param name="result"></param>
    public void SaveLog(string operateUserId, string operateAccount, string operateUserName, string IPAddress, string city, string host, string OS, string browser, string userAgent, bool result)
    {
        _loginlogsRepository.SaveLog(operateUserId, operateAccount, operateUserName, IPAddress, city, host, OS, browser, userAgent, result);
    }
    /// <summary>
    /// 保存Mvc端日志
    /// </summary>
    /// <param name="operateUserId"></param>
    /// <param name="operateAccount"></param>
    /// <param name="operateUserName"></param>
    /// <param name="IPAddress"></param>
    /// <param name="city"></param>
    /// <param name="host"></param>
    /// <param name="OS"></param>
    /// <param name="browser"></param>
    /// <param name="userAgent"></param>
    /// <param name="result"></param>
    public void SaveLog(string operateUserId, string operateAccount, string operateUserName, string IPAddress, string city, string host, string OS, string browser, string userAgent, string result)
    {
        _loginlogsRepository.SaveLog(operateUserId, operateAccount, operateUserName, IPAddress, city, host, OS, browser, userAgent, result);
    }
    #endregion
}
