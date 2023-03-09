namespace WebAdmin.Domain.EntityBase
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class VierificeCode
    {
        /// <summary>
        /// 验证码Base64
        /// </summary>
        public string? ImgBase64 { get; set; }
        /// <summary>
        /// uuid
        /// </summary>
        public string? uuid { get; set; }
#if DEBUG
        /// <summary>
        /// 验证码
        /// </summary>
        public string? VerificationCode { get; set; }
#else
#endif
    }
}
