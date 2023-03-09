namespace WebAdmin.Domain.Api.ViewModel
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 后台账户表主键Id
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 是否系统账户；拥有所有权限
        /// </summary>
        public bool IsSystem { get; set; }
        /// <summary>
        /// 登入时间
        /// </summary>
        public DateTime SignInTime { get; set; }
        /// <summary>
        /// JwtToken
        /// </summary>
        public string? JwtToken { get; set; }
    }
}
