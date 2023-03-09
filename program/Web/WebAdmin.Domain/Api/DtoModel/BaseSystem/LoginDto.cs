namespace WebAdmin.Domain.Api.DtoModel
{
    /// <summary>
    /// 登录接口 传入参数实体
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string? Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? PassWord { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string? VerificeCode { get; set; }
        /// <summary>
        /// 验证码uuid
        /// </summary>
        public string? uuid { get; set; }
    }
}
