using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Entity.Base
{
    /// <summary>
    /// 页面权限表
    /// </summary>
    [SugarTable("Base_Roles_User_Permission", TableDescription = "页面权限表")]
    public class RolesUserPermissionEntity
    {
        #region 实体成员
        /// <summary>
        /// 页面权限表Id
        /// </summary>   
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDataType = "varchar(50)", ColumnDescription = "页面权限表Id")]
        public string Id { get; set; }
        /// <summary>
        /// 页面菜单Id
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "页面菜单Id", IsNullable = true)]
        public string? MenuId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "角色Id", IsNullable = true)]
        public string? RoleId { get; set; }
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
