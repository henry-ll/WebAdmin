using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebAdmin.Framework.Attributes
{
    /// <summary>
    /// 仅允许Ajax请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// 忽略Ajax请求检测
        /// </summary>
        public bool Ignore { get; set; }
        /// <summary>
        /// 初始化仅允许Ajax请求
        /// </summary>
        /// <param name="ignore">跳过Ajax检测</param>
        public AjaxOnlyAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }
        /// <summary>
        /// 验证Ajax请求
        /// </summary>
        /// <param name="context">控制路由上下文</param>
        /// <param name="action">方法</param>
        public override bool IsValidForRequest(RouteContext context, ActionDescriptor action)
        {
            if (Ignore)
                return true;
            return context.HttpContext.Request.IsAjaxRequest();
        }
    }
}
