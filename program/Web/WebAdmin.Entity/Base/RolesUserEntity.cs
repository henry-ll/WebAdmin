using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.Entity.Base
{
    /// <summary>
    /// 用户角色对应关系表
    /// </summary>
    [SugarTable("Base_Roles_User", TableDescription = "用户角色对应关系表")]
    public class RolesUserEntity
    {
        #region 实体成员
        /// <summary>
        /// 用户角色对应关系表主键Id
        /// </summary>   
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDataType = "varchar(50)", ColumnDescription = "用户角色对应关系表主键Id")]
        public string Id { get; set; }
        /// <summary>
        /// 角色表主键Id
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "角色表主键Id", IsNullable = true)]
        public string? RoleId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "后台账户表主键Id", IsNullable = true)]
        public string? UserId { get; set; }
        #endregion

        #region 添加/编辑操作
        /// <summary>
        /// Mvc新增调用
        /// </summary>
        public void MvcCreate()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// Mvc编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void MvcModify(string keyValue)
        {
            this.Id = keyValue;
        }
        /// <summary>
        /// Api新增调用
        /// </summary>
        public void ApiCreate()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// Api编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void ApiModify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}
