namespace WebAdmin.Api.Filters
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
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(item => item is IAllowAnonymous))
                return;
            var result = await base.AuthorizationAsync(context);
            if (!result)
                return;
            string? token = context.HttpContext.Request.Headers[AppSetting.TokenHeaderName];
            token = token?.Replace("Bearer ", "");
            //判断是否存在token
            if (string.IsNullOrEmpty(token))
            {
                context.Result = Error("授权未通过", 401);
                return;
            }
            //单点登录
            var user = new UserService().GetEntityByToken(token);
            if (user == null)
            {
                context.Result = Error("该账号已在其他地方登录，请重新登录", 401);
                return;
            }
            if (string.IsNullOrWhiteSpace(user.JwtToKen))
            {
                context.Result = Error("登录已失效，请重新登录", 401);
                return;
            }
            await Task.CompletedTask;
        }
    }
}
