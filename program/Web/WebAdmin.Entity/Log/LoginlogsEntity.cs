using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Entity.Log
{
    /// <summary>
    /// 登录日志表
    /// </summary>
    [SugarTable("Log_Loginlogs", TableDescription = "登录日志表")]
    public class LoginlogsEntity
    {
        #region 实体成员
        /// <summary>
        /// 登录日志表主键Id
        /// </summary>   
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDataType = "varchar(50)", ColumnDescription = "登录日志表主键Id")]
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
        /// 操作人
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "操作人", IsNullable = true)]
        public string? OperateUserName { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "IP地址", IsNullable = true)]
        public string? IPAddress { get; set; }
        /// <summary>
        /// IP所在城市
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "IP所在城市", IsNullable = true)]
        public string? City { get; set; }
        /// <summary>
        /// 主机
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "主机", IsNullable = true)]
        public string? Host { get; set; }
        /// <summary>
        /// 系统版本
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "系统版本", IsNullable = true)]
        public string? OS { get; set; }
        /// <summary>
        /// 浏览器
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "浏览器", IsNullable = true)]
        public string? Browser { get; set; }
        /// <summary>
        /// 浏览器UserAgent
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(max)", ColumnDescription = "浏览器UserAgent", IsNullable = true)]
        public string? UserAgent { get; set; }
        /// <summary>
        /// 登录结果
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(max)", ColumnDescription = "登录结果", IsNullable = true)]
        public string? Result { get; set; }
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
        /// Api新增调用
        /// </summary>
        public void ApiCreate()
        {
            this.Id = Guid.NewGuid().ToString();
            this.LoginTime = DateTime.Now;
        }
        /// <summary>
        /// Api编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void ApiModify(string keyValue)
        {
        }
        #endregion 
    }
}
