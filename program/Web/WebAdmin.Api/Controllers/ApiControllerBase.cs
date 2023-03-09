using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Text;
using WebAdmin.Framework.Attributes;
using WebAdmin.Framework.Extentions;
using WebAdmin.Framework.Primitives;
using X.PagedList;

namespace WebAdmin.Api.Controllers
{
    /// <summary>
    /// Api控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns></returns>
        protected ContentResult JsonContent(string jsonStr)
        {
            return base.Content(jsonStr, "application/json", Encoding.UTF8);
        }
        /// <summary>
        /// 返回html
        /// </summary>
        /// <param name="body">html内容</param>
        /// <returns></returns>
        protected ContentResult HtmlContent(string body)
        {
            return base.Content(body);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        protected AjaxResult Success()
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = "请求成功！",
            };
            return res;
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        protected AjaxResult<T> Success<T>(T data)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = "请求成功！",
                Data = data
            };
            return res;
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        protected AjaxResult<T> Success<T>(T data, string msg)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = msg,
                Data = data
            };
            return res;
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        protected AjaxResult Error()
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                ResponseTime = DateTime.Now,
                Code =0,
                Msg = "请求失败！",
            };

            return res;
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        protected AjaxResult Error(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                ResponseTime = DateTime.Now,
                Code = 0,
                Msg = msg,
            };
            return res;
        }

        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns></returns>
        [HiddenApi]
        [HttpPost]
        protected virtual IActionResult IJsonContent(string jsonStr)
        {
            return Ok(base.Content(jsonStr, "application/json", Encoding.UTF8));
        }
        /// <summary>
        /// 返回html
        /// </summary>
        /// <param name="body">html内容</param>
        /// <returns></returns>
        [HiddenApi]
        [HttpPost]
        protected virtual IActionResult IHtmlContent(string body)
        {
            return base.Content(body);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        [HiddenApi]
        [HttpPost]
        protected virtual IActionResult ISuccess()
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = "请求成功！",
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        [HiddenApi]
        [HttpPost]
        protected virtual IActionResult ISuccess<T>(T data)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = "请求成功！",
                Data = data
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        [HiddenApi]
        [HttpPost]
        protected virtual IActionResult ISuccess<T>(IPagedList<T> data, string msg)
        {
            AjaxResultPagedList<T> res = new AjaxResultPagedList<T>
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = msg,
                Data = data,
                PageCount = data.PageCount,
                TotalItemCount = data.TotalItemCount,
                PageNumber = data.PageNumber,
                PageSize = data.PageSize,
                HasPreviousPage = data.HasPreviousPage,
                HasNextPage = data.HasNextPage,
                IsFirstPage = data.IsFirstPage,
                IsLastPage = data.IsLastPage,
                FirstItemOnPage = data.FirstItemOnPage,
                LastItemOnPage = data.LastItemOnPage
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="msg">返回消息</param>
        /// <returns></returns>
        [HiddenApi]
        [HttpPost]
        protected virtual IActionResult ISuccess<T>(T data, string msg)
        {
            AjaxResult<T> res = new AjaxResult<T>
            {
                Success = true,
                ResponseTime = DateTime.Now,
                Msg = msg,
                Data = data
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        [HiddenApi]
        [HttpPost]
        protected virtual IActionResult IError()
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                ResponseTime = DateTime.Now,
                Code = 0,
                Msg = "请求失败！",
            };
            return Ok(res);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        [HiddenApi]
        [HttpPost]
        protected virtual IActionResult IError(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                ResponseTime = DateTime.Now,
                Code = 0,
                Msg = msg,
            };
            return Ok(res);
        }
    }
}
