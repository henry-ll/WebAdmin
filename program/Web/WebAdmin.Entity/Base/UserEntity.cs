namespace WebAdmin.Entity.Base
{
    /// <summary>
    /// 后台账户表
    /// </summary>
    [SugarTable("Base_User", TableDescription = "后台账户表")]
    public class UserEntity
    {
        #region 实体成员
        /// <summary>
        /// 用户主键
        /// </summary>   
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnDataType = "varchar(50)", ColumnDescription = "用户主键Id")]
        public string Id { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(60)", ColumnDescription = "登录账户", IsNullable = true)]
        public string? Account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(60)", ColumnDescription = "登录密码", IsNullable = true)]
        public string? Password { get; set; }
        /// <summary>
        /// 密码秘钥
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(60)", ColumnDescription = "密码秘钥", IsNullable = true)]
        public string? Secretkey { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(125)", ColumnDescription = "真实姓名", IsNullable = true)]
        public string? RealName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "呢称", IsNullable = true)]
        public string? NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(200)", ColumnDescription = "头像", IsNullable = true)]
        public string? HeadIcon { get; set; }
        /// <summary>
        /// 性别(0女1男)
        /// </summary>      
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "性别", IsNullable = false)]
        public int Gender { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "联系电话", IsNullable = true)]
        public string? Mobile { get; set; }
        /// <summary>
        /// 所属组织（组织表主键Id）
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "所属组织（组织表主键Id）", IsNullable = true)]
        public string? OrganizeId { get; set; }
        /// <summary>
        /// 所属部门（部门表主键Id）
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(50)", ColumnDescription = "所属部门（部门表主键Id）", IsNullable = true)]
        public string? DepartmentId { get; set; }
        /// <summary>
        /// 暂停用户登录结束日期
        /// </summary>      
        [SugarColumn(ColumnDataType = "datetime", ColumnDescription = "暂停用户登录结束日期", IsNullable = true)]
        public DateTime? LockEndDate { get; set; }
        /// <summary>
        /// 上一次访问时间
        /// </summary>      
        [SugarColumn(ColumnDataType = "datetime", ColumnDescription = "上一次访问时间", IsNullable = true)]
        public DateTime? PreviousVisit { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>      
        [SugarColumn(ColumnDataType = "datetime", ColumnDescription = "最后访问时间", IsNullable = true)]
        public DateTime? LastVisit { get; set; }
        /// <summary>
        /// 登录总次数
        /// </summary>      
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "登录次数", IsNullable = false)]
        public int LogOnCount { get; set; }
        /// <summary>
        /// JwtToKen
        /// </summary>      
        [SugarColumn(ColumnDataType = "varchar(1500)", ColumnDescription = "JwtToKen", IsNullable = true)]
        public string? JwtToKen { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>      
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "登录次数", IsNullable = false)]
        public int LoginsNum { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>      
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "排序码", IsNullable = true)]
        public int? SortCode { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>      
        [SugarColumn(ColumnDataType = "int", ColumnDescription = "有效标志", IsNullable = true)]
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
