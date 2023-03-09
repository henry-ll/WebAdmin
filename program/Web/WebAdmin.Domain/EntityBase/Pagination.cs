namespace WebAdmin.Domain.EntityBase
{
    /// <summary>
    /// 
    /// </summary>
    public class Pagination: Base
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Pagination()
        {
            PageIndex = 1;
            PageSize = 15;
            IsShow = 0;
        }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序字段名
        /// </summary>
        public string? OrderFiled { get; set; }
        /// <summary>
        /// 排序类型（ASC 升序, DESC 降序）
        /// </summary>
        public string? OrderByType { get; set; } = "DESC";
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int Records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Total
        {
            get
            {
                if (Records > 0)
                    return Records % this.PageSize == 0 ? Records / this.PageSize : Records / this.PageSize + 1;
                else
                    return 0;
            }
        }
    }
}
