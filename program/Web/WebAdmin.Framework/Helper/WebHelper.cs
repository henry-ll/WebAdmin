using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using WebAdmin.Framework.Extentions;
using WebAdmin.Framework.Util;

namespace WebAdmin.Framework.Helper
{
    /// <summary>
    /// Web操作
    /// </summary>
    public static class WebHelper
    {
        #region Session操作
        /// <summary>
        /// 写Session
        /// </summary>
        /// <typeparam name="T">Session键值的类型</typeparam>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void WriteSession<T>(string key, T value)
        {
            HttpContext context = AppHttpContext.Current;
            if (string.IsNullOrWhiteSpace(key))
                return;
            if (context != null)
                context.Session.SetString(key, value.ToJson());
        }

        /// <summary>
        /// 写Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        /// <param name="value">Session的键值</param>
        public static void WriteSession(string key, string value)
        {
            HttpContext context = AppHttpContext.Current;
            if (string.IsNullOrWhiteSpace(key))
                return;
            if (context != null)
                context.Session.SetString(key, value);
        }

        /// <summary>
        /// 读取Session的值
        /// </summary>
        /// <param name="key">Session的键名</param>        
        public static string GetSession(string key)
        {
            HttpContext context = AppHttpContext.Current;
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;
            if (context != null)
                return context.Session.GetString(key);
            return string.Empty;
        }
        /// <summary>
        /// 删除指定Session
        /// </summary>
        /// <param name="key">Session的键名</param>
        public static void RemoveSession(string key)
        {
            HttpContext context = AppHttpContext.Current;
            if (string.IsNullOrWhiteSpace(key))
                return;
            if (context != null)
                context.Session.Remove(key);
        }

        #endregion

        #region Cookie操作
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            WriteCookie(strName, strValue, 0);
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            try
            {
                HttpContext context = AppHttpContext.Current;
                if (context.Request.Cookies[strName] != null)
                {
                    context.Response.Cookies.Delete(strName);
                }
                context.Response.Cookies.Append(strName, strValue, new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(expires),
                    HttpOnly = true
                }); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string? GetCookie(string strName)
        {
            string cs = "";
            HttpContext context = AppHttpContext.Current;
            if (context.Request.Cookies[strName] != null)
                cs = context.Request.Cookies[strName];
            return cs;
        }
        /// <summary>
        /// 删除Cookie对象
        /// </summary>
        /// <param name="CookiesName">Cookie对象名称</param>
        public static void RemoveCookie(string CookiesName)
        {
            HttpContext context = AppHttpContext.Current;
            if (context.Request.Cookies[CookiesName] != null)
            {
                context.Response.Cookies.Delete(CookiesName);
            }
        }
        #endregion

        #region HttpWebRequest(请求网络资源)
        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源地址</param>
        public static string HttpWebRequest(string url)
        {
            return HttpWebRequest(url, string.Empty, Encoding.GetEncoding("utf-8"));
        }
        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="contentType">内容类型</param>
        public static string HttpWebRequestURL(string url, string contentType)
        {
            return HttpWebRequest(url, string.Empty, Encoding.GetEncoding("utf-8"), true, contentType);
        }
        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源Url地址</param>
        /// <param name="parameters">提交的参数,格式：参数1=参数值1&amp;参数2=参数值2</param>
        public static string HttpWebRequest(string url, string parameters)
        {
            return HttpWebRequest(url, parameters, Encoding.GetEncoding("utf-8"), true);
        }
        /// <summary>
        /// 请求网络资源,返回响应的文本json格式
        /// </summary>
        /// <param name="url">网络资源Url地址</param>
        /// <param name="parameters">提交的参数,格式：参数1=参数值1&amp;参数2=参数值2</param>
        /// <param name="contentType">内容类型</param>
        public static string HttpWebRequest(string url, string parameters, string contentType)
        {
            return HttpWebRequest(url, parameters, Encoding.GetEncoding("utf-8"), true, contentType);
        }
        /// <summary>
        /// 请求网络资源,返回响应的文本
        /// </summary>
        /// <param name="url">网络资源地址</param>
        /// <param name="parameters">提交的参数,格式：参数1=参数值1&amp;参数2=参数值2</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isPost">是否Post提交</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="cookie">Cookie容器</param>
        /// <param name="timeout">超时时间</param>
        public static string HttpWebRequest(string url, string parameters, Encoding encoding, bool isPost = false,
             string contentType = "application/x-www-form-urlencoded", CookieContainer cookie = null, int timeout = 120000)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = timeout;
            request.CookieContainer = cookie;
            if (isPost)
            {
                byte[] postData = encoding.GetBytes(parameters);
                request.Method = "POST";
                request.ContentType = contentType;
                request.ContentLength = postData.Length;
                using Stream stream = request.GetRequestStream();
                stream.Write(postData, 0, postData.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            string result;
            using (Stream stream = response.GetResponseStream())
            {
                if (stream == null)
                    return string.Empty;
                using var reader = new StreamReader(stream, encoding);
                result = reader.ReadToEnd();
            }
            return result;
        }

        #endregion

        #region 去除HTML标记
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHtml(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&hellip;", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&mdash;", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "", RegexOptions.IgnoreCase);
            _ = Htmlstring.Replace("<", "");
            Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "", RegexOptions.IgnoreCase);
            _ = Htmlstring.Replace(">", "");
            _ = Htmlstring.Replace("\r\n", "");
            return Htmlstring;
        }
        /// <summary>
        /// 对转义字符进行处理
        /// 左尖括号: < &lt;
        /// 右尖括号: > &gt;
        /// 单引号  : ' &apos;
        /// 双引号  : " &quot; 
        /// (shift+7):& &amp; 
        /// </summary>
        public class TransferredMeaning
        {
            static public string Transferred(string Meaning)
            {
                //普通字符变换成转义字符
                Meaning = Meaning.Replace("&", "&amp;");
                Meaning = Meaning.Replace("<", "&lt;");
                Meaning = Meaning.Replace(">", "&gt;");
                Meaning = Meaning.Replace("'", "&apos;");
                Meaning = Meaning.Replace("\"", "&quot;");
                return Meaning;
            }
        }
        #endregion
    }
}
