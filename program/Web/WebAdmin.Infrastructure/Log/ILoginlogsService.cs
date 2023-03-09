using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Entity.Log;
using WebAdmin.Framework.Extentions.Exceptions;
using WebAdmin.Framework.Extentions;
using X.PagedList;

namespace WebAdmin.Infrastructure.Log;

/// <summary>
/// 登录日志
/// </summary>
public interface ILoginlogsService : IBaseService<LoginlogsEntity>
{
    #region 获取数据
    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="queryJson"></param>
    /// <returns></returns>
    IEnumerable<LoginlogsEntity> GetList(string queryJson);
    /// <summary>
    /// 分页 列表
    /// </summary>
    /// <param name="pagination"></param>
    /// <param name="queryJson"></param>
    /// <returns></returns>
    IPagedList<LoginlogsEntity> GetPagedList(Pagination pagination, string queryJson);
    #endregion

    #region 验证数据

    #endregion

    #region 保存数据
    /// <summary>
    /// 保存日志
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
    void SaveLog(string operateUserId, string operateAccount, string operateUserName, string IPAddress, string city, string host, string OS, string browser, string userAgent, bool result);
    /// <summary>
    /// 保存日志
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
    void SaveLog(string operateUserId, string operateAccount, string operateUserName, string IPAddress, string city, string host, string OS, string browser, string userAgent, string result);
    #endregion
}
