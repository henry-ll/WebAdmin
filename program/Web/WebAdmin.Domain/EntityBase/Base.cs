using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Domain.EntityBase
{
    /// <summary>
    /// 基类
    /// </summary>
    public class Base
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string? Categorykey { get; set; } = null ?? "";
        /// <summary>
        /// 是否首次打开页面时展示数据(0否，1是)
        /// </summary>
        public int IsShow { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? STime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ETime { get; set; }
    }
}
