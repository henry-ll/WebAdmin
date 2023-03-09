using WebAdmin.Framework.Configs;
using WebAdmin.Framework.Operators;

namespace WebAdmin.Framework.Providers
{
    /// <summary>
    /// 操作者回话接口
    /// </summary>
    public interface IOperatorProvider
    {
        /// <summary>
        /// Mvc项目：写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        void AddMvcCurrent(JwtOperator user);
        /// <summary>
        /// Mvc项目：写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        /// <param name="expires">过期时间 （分钟）</param>
        void AddMvcCurrent(JwtOperator user,int expires= 60 * 24 * 7);
        /// <summary>
        /// Mvc项目：获取当前用户信息
        /// </summary>
        /// <returns></returns>
        JwtOperator MvcCurrent();
        /// <summary>
        /// Api项目：获取当前用户信息
        /// </summary>
        /// <returns></returns>
        JwtOperator? ApiCurrent();
        /// <summary>
        /// 删除当前用户登录缓存
        /// </summary>
        void EmptyCurrent();
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        bool IsOverdue();
    }
}
