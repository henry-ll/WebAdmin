using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Entity.Log;
using WebAdmin.Infrastructure.Log;
using X.PagedList;

namespace WebAdmin.Repositories.Log;

/// <summary>
/// 登录日志
/// </summary>
public class LoginlogsRepository : BaseRepository<LoginlogsEntity>
{

    #region 获取数据
    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="queryJson"></param>
    /// <returns></returns>
    public IEnumerable<LoginlogsEntity> GetList(string queryJson)
    {
        var expression = LinqExtension.True<LoginlogsEntity>();
        var queryParam = queryJson.ToJObject();
        //查询条件
        if (!queryParam["keyword"].IsEmpty())
        {
            string keyword = queryParam["keyword"].ToString();
            expression = expression.And(t => t.OperateAccount.Contains(keyword) || t.OperateUserName.Contains(keyword) || t.City.Contains(keyword) || t.Host.Contains(keyword) || t.OS.Contains(keyword) || t.Browser.Contains(keyword));
        }
        return this.BaseRepository().FindList(expression);
    }
    /// <summary>
    /// 分页 列表
    /// </summary>
    /// <param name="pagination"></param>
    /// <param name="queryJson"></param>
    /// <returns></returns>
    public IPagedList<LoginlogsEntity> GetPagedList(Pagination pagination, string queryJson)
    {
        var expression = LinqExtension.True<LoginlogsEntity>();
        var queryParam = queryJson.ToJObject();
        //查询条件
        if (!queryParam["keyword"].IsEmpty())
        {
            string keyword = queryParam["keyword"].ToString();
            expression = expression.And(t => t.OperateAccount.Contains(keyword) || t.OperateUserName.Contains(keyword) || t.City.Contains(keyword) || t.Host.Contains(keyword) || t.OS.Contains(keyword) || t.Browser.Contains(keyword));
        }
        return this.BaseRepository().ToPagedList(expression, pagination);
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
        var entity = new LoginlogsEntity()
        {
            OperateUserId = operateUserId,
            OperateAccount = operateAccount,
            OperateUserName = operateUserName,
            IPAddress = IPAddress,
            City = city,
            Host = host,
            OS = OS,
            Browser = browser,
            UserAgent = userAgent,
            Result = result ? "登录成功" : "登录失败"
        };
        entity.MvcCreate();
        this.BaseRepository().Insert(entity);
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
        var entity = new LoginlogsEntity()
        {
            OperateUserId = operateUserId,
            OperateAccount = operateAccount,
            OperateUserName = operateUserName,
            IPAddress = IPAddress,
            City = city,
            Host = host,
            OS = OS,
            Browser = browser,
            UserAgent = userAgent,
            Result = result
        };
        entity.MvcCreate();
        this.BaseRepository().Insert(entity);
    }
    #endregion

}
