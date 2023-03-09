using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Framework.Operators
{
    /// <summary>
    /// 后台操作者 实体
    /// </summary>
    public class JwtOperator
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
        /// 账户
        /// </summary>
        public string? Account { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>      
        public string? Phone { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>      
        public string? IdNumber { get; set; }
        /// <summary>
        /// 公司表主键Id
        /// </summary>
        public string? CompanyId { get; set; }
        /// <summary>
        /// 机构表主键Id
        /// </summary>      
        public string? OrganizeId { get; set; }
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
        /// <summary>
        /// 登录端
        /// </summary>
        public int LoginMode { get; set; }

    }
}
