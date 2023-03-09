using Microsoft.AspNetCore.Http;
using ServiceStack;
using System;
using WebAdmin.Framework.Caches;
using WebAdmin.Framework.Configs;
using WebAdmin.Framework.Extentions;
using WebAdmin.Framework.Helper;
using WebAdmin.Framework.Operators;
using WebAdmin.Framework.Util;
using Newtonsoft.Json;
namespace WebAdmin.Framework.Providers
{
    /// <summary>
    /// 后台操作者会话
    /// </summary>
    public class OperatorProvider : IOperatorProvider
    {
        #region 静态实例
        /// <summary>
        /// 当前提供者
        /// </summary>
        public static IOperatorProvider Provider
        {
            get
            {
                return new OperatorProvider();
            }
        }
        #endregion

        /// <summary>
        /// Cookie/Session的key值名称
        /// </summary>
        private string keyName =Config.GetValue("SCKey")?.Trim();
        /// <summary>
        /// 登陆提供者模式:Session、Cookie 
        /// </summary>
        private string? LoginProvider = Config.GetValue("LoginProvider")?.Trim();
        /// <summary>
        /// 过期时间 （分钟）
        /// </summary>
        private int expires = 60 * 24 * 7;
        /// <summary>
        /// Mvc项目：写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        public virtual void AddMvcCurrent(JwtOperator user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LoginProvider)) 
                    throw new Exception("系统配置出错");
                if (LoginProvider == "Cookie")
                {
                    string cookie = new CryptoHelper().AesEncrypt(JsonConvert.SerializeObject(user), CryptoHelper.AesKey);
                    WebHelper.WriteCookie(keyName, cookie, expires);
                }
                else if(LoginProvider == "Session")
                {
                    string session = new CryptoHelper().AesEncrypt(JsonConvert.SerializeObject(user), CryptoHelper.AesKey);
                    WebHelper.WriteSession(keyName, session);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Mvc项目：写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        /// <param name="expires">过期时间 （分钟）</param>
        public virtual void AddMvcCurrent(JwtOperator user, int expires = 60 * 24 * 7)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LoginProvider))
                    throw new Exception("系统配置出错");
                if (LoginProvider == "Cookie")
                {
                    string cookie = new CryptoHelper().AesEncrypt(JsonConvert.SerializeObject(user), CryptoHelper.AesKey);
                    WebHelper.WriteCookie(keyName, cookie, expires);
                }
                else if (LoginProvider == "Session")
                {
                    string session = new CryptoHelper().AesEncrypt(JsonConvert.SerializeObject(user), CryptoHelper.AesKey);
                    WebHelper.WriteSession(keyName, session);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Mvc项目：获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public virtual JwtOperator MvcCurrent()
        {
            try
            {
                if (LoginProvider == "Cookie")
                    return new CryptoHelper().AesDecrypt(keyName, WebHelper.GetCookie(keyName).ToString()).ToObject<JwtOperator>();
                else if (LoginProvider == "Session")
                    return new CryptoHelper().AesDecrypt(keyName, WebHelper.GetSession(keyName).ToString()).ToObject<JwtOperator>();
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Api项目：获取当前用户信息
        /// </summary>
        /// <returns></returns>
        public virtual JwtOperator? ApiCurrent()
        {
            try
            {
                HttpContext context = AppHttpContext.Current;
                if (context == null)
                    return null;
                if (context.Items["UserInfo"] == null)
                    return null;
                JwtOperator operators = JsonConvert.DeserializeObject<JwtOperator>(context.Items["UserInfo"].ToString());
                return operators;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 删除当前用户登录缓存
        /// </summary>
        public virtual void EmptyCurrent()
        {
            if (LoginProvider == "Cookie")
                WebHelper.RemoveCookie(keyName);
            else if(LoginProvider == "Session")
                WebHelper.RemoveSession(keyName);
        }
        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public virtual bool IsOverdue()
        {
            try
            {
                string? str = "";
                if (LoginProvider == "Cookie")
                    str = WebHelper.GetCookie(keyName);
                else if (LoginProvider == "Session")
                    str = WebHelper.GetSession(keyName);
                if (string.IsNullOrWhiteSpace(str))
                    return true;
                if (!string.IsNullOrWhiteSpace(str))
                    return false;
                else
                {
                    var user = DESEncrypt.Decrypt(str.ToString()).ToObject<JwtOperator>();
                    try
                    {
                        if (user.SignInTime.AddMinutes(expires) < DateTime.Now)
                            return false;
                    }
                    catch
                    {
                        return true;
                    }
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }
    }
}
