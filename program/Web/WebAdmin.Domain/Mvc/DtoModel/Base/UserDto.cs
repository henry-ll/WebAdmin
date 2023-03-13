using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Domain.Mvc.DtoModel
{
    /// <summary>
    /// User
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string? inputaccount { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? inputname { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string? inputcontact { get; set; }
        /// <summary>
        /// 所属组织
        /// </summary>
        public string? selectorganize { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string? selectdepartment { get; set; }
        /// <summary>
        /// 有效标识
        /// </summary>
        public int? radioenabledmark { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? radiosex { get; set; }
    }
}
