using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Framework.Primitives
{
    public class AjaxResultPagedList<T> : AjaxResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public IPagedList<T> Data { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalItemCount { get; set; }

        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页多少条
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// 是否是首页
        /// </summary>
        public bool IsFirstPage { get; set; }

        /// <summary>
        /// 是否是尾页
        /// </summary>
        public bool IsLastPage { get; set; }

        /// <summary>
        /// One-based index of the first item in the paged subset, zero if the superset is empty or PageNumber
        /// is greater than PageCount.
        /// </summary>
        /// <value>
        /// One-based index of the first item in the paged subset, zero if the superset is empty or PageNumber
        /// is greater than PageCount.
        /// </value>
        public int FirstItemOnPage { get; set; }

        /// <summary>
        /// One-based index of the last item in the paged subset, zero if the superset is empty or PageNumber
        /// is greater than PageCount.
        /// </summary>
        /// <value>
        /// One-based index of the last item in the paged subset, zero if the superset is empty or PageNumber
        /// is greater than PageCount.
        /// </value>
        public int LastItemOnPage { get; set; }
    }
}
