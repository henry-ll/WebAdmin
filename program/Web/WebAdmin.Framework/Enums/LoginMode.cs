using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Framework.Enums
{
    /// <summary>
    /// 登录认证模式
    /// </summary>
    public enum LoginMode
    {
        /// <summary>
        ///PC端 Web Mvc后台
        /// </summary>
        PCWebMvc = 1,
        /// <summary>
        /// PC端 Web Api
        /// </summary>
        PCWebApi = 2,

    }
}
