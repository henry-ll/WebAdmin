using NuGet.Protocol.Core.Types;
using SugarRedis;
using System.Runtime.InteropServices;

namespace WebAdmin.Mvc.Controllers
{
    /// <summary>
    /// Mvc控制器基类
    /// </summary>
    public class MvcControllerBase : Controller
    {
        protected string? City;
        protected string? Ip;
        protected string? HostName;
        protected string? OS;
        protected string? Browser;
        protected string? User_Agent;
        private IIPLocatorProvider _ipLocatorProvider { get; set; }
        /// <summary>
        /// 初始化 依赖注入
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="ipLocatorProvider"></param>
        public MvcControllerBase(IIPLocatorProvider IPLocatorProvider)
        {
            _ipLocatorProvider = IPLocatorProvider;
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        protected virtual IActionResult Success()
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = "请求成功！",
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        [HttpPost]
        protected virtual IActionResult Success<T>(T data)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = "请求成功！",
                Data = data
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        [HttpPost]
        protected virtual IActionResult Success<T>(T data, string msg)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = msg,
                Data = data
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        protected virtual IActionResult Error()
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                ResponseTime = DateTime.Now,
                Code = 0,
                Msg = "请求失败！",
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        [HttpPost]
        protected virtual IActionResult Error(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                ResponseTime = DateTime.Now,
                Code = 0,
                Msg = msg,
            };
            return Ok(res);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
             base.OnActionExecuting(context);
            GetClientInfo(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void GetClientInfo(ActionExecutingContext context)
        {
            Ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            City =  _ipLocatorProvider.Locate(Ip).Result;
            HostName = Host();
            OS = RuntimeInformation.OSDescription;
            // 获取 User-Agent 头信息
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            User_Agent = userAgent;
            var uap = new Netnr.UAParser.Parsers(userAgent);
            var clientEntity = uap.GetClient();
            Browser = clientEntity?.Name + " " + clientEntity?.Version;
            var osEntity = uap.GetOS();
            OS = osEntity?.Name + " " + osEntity?.Version;
            //var deviceEntity = uap.GetDevice();
            //var botEntity = uap.GetBot();
        }

        #region GetHost
        /// <summary>
        /// 获取主机名
        /// </summary>
        protected virtual string Host()
        {
            try
            {
                HttpContext context = AppHttpContext.Current;
                return context == null ? Dns.GetHostName() : GetWebClientHostName();
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取Web客户端主机名
        /// </summary>
        protected virtual string GetWebClientHostName()
        {
            var ip = GetWebRemoteIp();
            var result = Dns.GetHostEntry(IPAddress.Parse(ip)).HostName;
            if (result == "localhost.localdomain")
                result = Dns.GetHostName();
            return result;
        }
        /// <summary>
        /// 获取Web远程Ip
        /// </summary>
        protected virtual string GetWebRemoteIp()
        {
            try
            {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
