using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Polly;
using Senparc.CO2NET.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using WebAdmin.Framework.Configs;
using WebAdmin.Framework.Enums;
using WebAdmin.Framework.Exceptions;
using WebAdmin.Framework.Extentions;
using WebAdmin.Framework.Helper;
using WebAdmin.Framework.Operators;

namespace WebAdmin.Framework.Exceptions
{
    /// <summary>
    /// Jwt鉴权
    /// </summary>
    public class JwtAuthorizeFilter : BaseActionFilterAsync, IAsyncAuthorizationFilter
    {
        private LoginMode _LoginMode;
        protected static readonly string CookiesKey = Config.GetValue("SCKey")?.Trim();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoginMode"></param>
        public JwtAuthorizeFilter(LoginMode LoginMode)
        {
            this._LoginMode = LoginMode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(item => item is IAllowAnonymous))
                return;
            else if (_LoginMode == LoginMode.PCWebApi)
                NextPCWebApi(context);
            else if (_LoginMode == LoginMode.PCWebMvc)
                NextPCWebMvc(context);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<bool> AuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(item => item is IAllowAnonymous))
                return true;
            else if (_LoginMode == LoginMode.PCWebApi)
                return NextPCWebApi(context);
            else if (_LoginMode == LoginMode.PCWebMvc)
                return NextPCWebMvc(context);
            await Task.CompletedTask;
            return false;
        }

        /// <summary>
        /// PCWebApi执行方法
        /// </summary>
        /// <param name="context"></param>
        public bool NextPCWebApi(AuthorizationFilterContext context)
        {
            string? token = context.HttpContext.Request.Headers[AppSetting.TokenHeaderName].ToString()?.Replace("Bearer ", "");
            //判断是否存在token
            if (string.IsNullOrEmpty(token))
            {
                context.Result = Error("授权未通过", 401);
                return false;
            }
            //判断是否过期
            if (JwtHelper.IsExp(token))
            {
                context.Result = Error("身份凭证失效，请重新登录", 401);
                return false;
            }
            //解析token
            string userId = JwtHelper.GetUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = 401;
                var dobj = new { message = "授权未通过", status = false, code = 401 };
                context.Result = new JsonResult(dobj);
                return false;
            }
            JwtOperator operators = JwtHelper.SerializeJwt(token).ToObject<JwtOperator>();
            if ((int)this._LoginMode != operators.LoginMode)
            {
                context.Result = Error("授权未通过", 401);
                return false;
            }
            context.HttpContext.Items.Add("UserInfo", operators.ToJson());
            return true;
        }
        /// <summary>
        /// PCWebMvc执行方法
        /// </summary>
        /// <param name="context"></param>
        public bool NextPCWebMvc(AuthorizationFilterContext context)
        {
            CryptoHelper crypto = new CryptoHelper();
            context.HttpContext.Request.Cookies.TryGetValue(CookiesKey, out string? strAdmin);
            if (string.IsNullOrWhiteSpace(strAdmin))
            {
                context.Result = new RedirectResult("/Login/Index");
                return false;
            }
            strAdmin = crypto.AesDecrypt(strAdmin, CryptoHelper.AesKey);
            JwtOperator? operators = JsonConvert.DeserializeObject<JwtOperator>(strAdmin);
            if (operators == null)
            {
                context.Result = new RedirectResult("/Login/Index");
                return false;
            }
            if (string.IsNullOrWhiteSpace(operators.JwtToken) || string.IsNullOrWhiteSpace(operators.UserId))
            {
                context.Result = new RedirectResult("/Login/Index");
                return false;
            }
            string token = operators.JwtToken.Replace("Bearer ", "");
            //判断是否传入了token
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectResult("/Login/Index");
                return false;
            }
            //解析token
            string userId = JwtHelper.GetUserId(token);
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new RedirectResult("/Login/Index");
                return false;
            }
            JwtOperator jwtoperators = JwtHelper.SerializeJwt(token).ToObject<JwtOperator>();
            if (jwtoperators==null||string.IsNullOrWhiteSpace(jwtoperators.UserId))
            {
                context.Result = new RedirectResult("/Login/Index");
                return false;
            }
            if ((int)this._LoginMode != jwtoperators.LoginMode)
            {
                context.Result = new RedirectResult("/Login/Index");
                return false;
            }
            return true;
        }
    }
}
