using Microsoft.AspNetCore.Mvc;

namespace WebAdmin.Mvc.Controllers.Basics
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        /// <summary>
        /// 无权限页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Error401()
        {
            return View();
        }
        /// <summary>
        /// 页面找不到
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Error404()
        {
            return View();
        }
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Error500()
        {
            return View();
        }
    }
}
