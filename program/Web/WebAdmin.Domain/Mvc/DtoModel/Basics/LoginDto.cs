using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Domain.Mvc.DtoModel
{
    /// <summary>
    /// 登录接口 传入参数实体
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string? username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? md5_password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string? verifyCode { get; set; }
        /// <summary>
        /// 验证码uuid
        /// </summary>
        public string? uuid { get; set; }
    }
}
