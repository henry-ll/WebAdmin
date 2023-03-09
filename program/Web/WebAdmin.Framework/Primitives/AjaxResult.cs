namespace WebAdmin.Framework.Primitives
{
    /// <summary>
    /// Ajax请求结果
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// 回复时间
        /// </summary>
         public DateTime ResponseTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 0失败，1成功
        /// </summary>
        public int Code { get; set; } =1;
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }
    }
}
