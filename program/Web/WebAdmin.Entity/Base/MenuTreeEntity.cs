using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Entity.Base
{
    /// <summary>
    /// 页面菜单表
    /// </summary>
    [SugarTable("Base_Menu_Tree", TableDescription = "页面菜单表")]
    public class MenuTreeEntity
    {
        #region 实体成员
        /// <summary>
        /// 页面菜单表主键Id
        /// </summary>   
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDataType = "varchar(50)", ColumnDescription = "页面菜单表主键Id")]
        public string Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "菜单名称", IsNullable = true)]
        public string? MenuName { get; set; }
        /// <summary>
        /// 父级主键Id
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "父级主键Id", IsNullable = true)]
        public string? ParentId { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "路由地址", IsNullable = true)]
        public string? CoreUrl { get; set; }
        /// <summary>
        /// 图标
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "图标", IsNullable = true)]
        public string? Icon { get; set; }
        /// <summary>
        /// 排序
        /// </summary>      
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "排序", IsNullable = true)]
        public int? SortCode { get; set; }
        /// <summary>
        /// 菜单Class
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(255)", ColumnDescription = "菜单Class", IsNullable = true)]
        public string? TargetClass { get; set; }
        /// <summary>
        /// 有效标志（0无效，1有效）
        /// </summary>      
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "有效标志（0无效，1有效）", IsNullable = true)]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(2000)", ColumnDescription = "备注", IsNullable = true)]
        public string? Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>      
        [SugarColumn(ColumnDataType = "datetime", ColumnDescription = "创建日期", IsNullable = true)]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "创建用户主键", IsNullable = true)]
        public string? CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "创建用户", IsNullable = true)]
        public string? CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>      
        [SugarColumn(ColumnDataType = "datetime", ColumnDescription = "修改日期", IsNullable = true)]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "修改用户主键", IsNullable = true)]
        public string? ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "修改用户", IsNullable = true)]
        public string? ModifyUserName { get; set; }
        #endregion

        #region 添加/编辑操作
        /// <summary>
        /// Mvc新增调用
        /// </summary>
        public void MvcCreate()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.EnabledMark = 1;
            this.CreateUserId = OperatorProvider.Provider.MvcCurrent()?.UserId;
            this.CreateUserName = OperatorProvider.Provider.MvcCurrent()?.UserName;
        }
        /// <summary>
        /// Mvc编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void MvcModify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.MvcCurrent()?.UserId;
            this.ModifyUserName = OperatorProvider.Provider.MvcCurrent()?.UserName;
        }
        /// <summary>
        /// Api新增调用
        /// </summary>
        public void ApiCreate()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.EnabledMark = 1;
            this.CreateUserId = OperatorProvider.Provider.ApiCurrent()?.UserId;
            this.CreateUserName = OperatorProvider.Provider.ApiCurrent()?.UserName;
        }
        /// <summary>
        /// Api编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void ApiModify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.ApiCurrent()?.UserId;
            this.ModifyUserName = OperatorProvider.Provider.ApiCurrent()?.UserName;
        }
        #endregion
    }
}
