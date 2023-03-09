using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Entity.Log
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("Log_DBlogs", TableDescription = "DB执行语句表")]
    public class DBlogsEntity
    {
        ///
        #region 实体成员
        /// <summary>
        /// DB执行语句表主键Id
        /// </summary>   
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDataType = "varchar(50)", ColumnDescription = "DB执行语句表主键Id")]
        public string Id { get; set; }
        /// <summary>
        /// 操作用户Id
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "操作用户Id", IsNullable = true)]
        public string? OperateUserId { get; set; }
        /// <summary>
        /// 操作账号
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "操作账号", IsNullable = true)]
        public string? OperateAccount { get; set; }
        /// <summary>
        /// 执行的sql语句
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(max)", ColumnDescription = "执行的sql语句", IsNullable = true)]
        public string? SqlExecute { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>      
        [SugarColumn(ColumnDataType = "datetime", ColumnDescription = "登录时间", IsNullable = true)]
        public DateTime? LoginTime { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// Mvc新增调用
        /// </summary>
        public void MvcCreate()
        {
            this.Id = Guid.NewGuid().ToString();
            this.LoginTime = DateTime.Now;
        }
        /// <summary>
        /// Mvc编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void MvcModify(string keyValue)
        {
        }
        /// <summary>
        /// Mvc新增调用
        /// </summary>
        public void ApiCreate()
        {
            this.Id = Guid.NewGuid().ToString();
            this.LoginTime = DateTime.Now;
        }
        /// <summary>
        /// Mvc编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void ApiModify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}
