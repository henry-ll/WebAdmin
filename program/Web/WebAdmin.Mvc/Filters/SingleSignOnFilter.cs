using Newtonsoft.Json;

namespace WebAdmin.Mvc.Filters
{
    /// <summary>
    /// 单点登录Filter
    /// </summary>
    public class SingleSignOnFilter :JwtAuthorizeFilter, IAsyncAuthorizationFilter
    {
        private LoginMode _LoginMode;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="LoginMode"></param>
        public SingleSignOnFilter(LoginMode LoginMode):base(LoginMode)
        {
            this._LoginMode = LoginMode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public  async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(item => item is IAllowAnonymous))
                return;
            var result =  await base.AuthorizationAsync(context);
            if (!result)
                return;
            context.HttpContext.Request.Cookies.TryGetValue(CookiesKey, out string? strAdmin);
            CryptoHelper crypto = new CryptoHelper();
            strAdmin = crypto.AesDecrypt(strAdmin, CryptoHelper.AesKey);
            JwtOperator? operators = JsonConvert.DeserializeObject<JwtOperator>(strAdmin);
            string token = operators.JwtToken.Replace("Bearer ", "");
            //单点登录
            var user = new UserService().GetEntity(operators.UserId);
            if (user == null)
            {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }
            if (user.EnabledMark == null || user.EnabledMark == 0)//账号被禁用
            {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }
            if (string.IsNullOrWhiteSpace(user.JwtToKen))//token为空
            {
                context.Result = new RedirectResult("/Login/Index");
                return;
            }
            var userByToken = new UserService().GetEntityByToken(token);
            if (userByToken == null)
            {
                //该账号已在其他地方登录，请重新登录
                context.Result = new RedirectResult("/Login/Index?messtype=1");
                return;
            }
            if (string.IsNullOrWhiteSpace(user.JwtToKen))
            {
                //登录已失效，请重新登录
                context.Result = new RedirectResult("/Login/Index?messtype=2");
                return;
            }
            await Task.CompletedTask;
        }
    }
}
