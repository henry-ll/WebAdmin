using System.Data;
using System.Text;
using WebAdmin.Framework.Helper;

namespace CodeGenerator
{
    /// <summary>
    /// 代码生成模板(单表)
    /// </summary>
    public class SingleTable
    {
        #region 实体类
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="baseConfigModel">基本信息</param>
        /// <param name="dt">实体字段</param>
        /// <returns></returns>
        public string EntityBuilder(BaseConfigModel baseConfigModel, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;\r\n");
            sb.Append("using Application.Code.Operator;\r\n");
            sb.Append("using SqlSugar;\r\n\r\n");
            sb.Append("namespace Application.Entity." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// " + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");



            sb.Append("    [SugarTable(\"" + baseConfigModel.DataBaseTableName + "\", TableDescription = \"" + baseConfigModel.Description + "\")]\r\n");
            sb.Append("    public class " + baseConfigModel.EntityClassName + " : BaseEntity\r\n");
            sb.Append("    {\r\n");
            sb.Append("        #region 实体成员\r\n");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rowItem in dt.Rows)
                {
                    string column = rowItem["column"].ToString();
                    string remark = rowItem["remark"].ToString();
                    string datatype = DbTypeConvertHelper.FindModelsType(rowItem["datatype"].ToString());
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// " + remark + "\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    string strlen = rowItem["length"].ToString();
                    if (strlen == "-1")
                    {
                        strlen = "max";
                    }
                    string lenstr = "";
                    if (datatype == "string")
                    {
                        lenstr = " ColumnDataType = \"" + rowItem["datatype"].ToString() + "(" + strlen + ")\", ";
                    }
                    if (column == baseConfigModel.DataBaseTablePK)
                    {
                        sb.Append("        [SugarColumn(" + lenstr + "IsPrimaryKey = true,ColumnDescription = \"" + remark + "\")]\r\n");
                    }
                    else
                    {
                        sb.Append("        [SugarColumn(" + lenstr + "ColumnDescription = \"" + remark + "\",IsNullable = " + (rowItem["isnullable"].ToString() == "1" ? "true" : "false") + ")]\r\n");
                    }
                    sb.Append("        public " + datatype + " " + column + " { get; set; }\r\n");
                }
            }
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 扩展操作\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 新增调用\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        public override void Create()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            this." + baseConfigModel.DataBaseTablePK + " = Guid.NewGuid().ToString();\r\n");
            sb.Append("            " + IsCreateDate(dt) + "");
            string isuser = IsCreateUserId(dt);
            string isusername = IsCreateUserName(dt);
            if (isuser != "" || isusername != "")
            {
                sb.Append("            try                         \r\n");
                sb.Append("            {                           \r\n");
            }

            sb.Append("               " + IsCreateUserId(dt) + "");
            sb.Append("               " + IsCreateUserName(dt) + "");
            if (isuser != "" || isusername != "")
            {
                sb.Append("            }                           \r\n");
                sb.Append("            catch (Exception)           \r\n");
                sb.Append("            {                           \r\n"); ;
                sb.Append("            }                           \r\n");
            }

            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 编辑调用\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
            sb.Append("        public override void Modify(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            this." + baseConfigModel.DataBaseTablePK + " = keyValue;\r\n");
            sb.Append("            " + IsModifyDate(dt) + "");
            sb.Append("            " + IsModifyUserId(dt) + "");
            sb.Append("            " + IsModifyUserName(dt) + "");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}");
            return sb.ToString();
        }
        public string IsCreateDate(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'CreateDate'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateDate = DateTime.Now;\r\n";
            }
            return "";
        }
        public string IsCreateUserId(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'CreateUserId'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateUserId = OperatorProvider.Provider.Current().UserId;\r\n";
            }
            return "";
        }
        public string IsCreateUserName(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'CreateUserName'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateUserName = OperatorProvider.Provider.Current().UserName;\r\n";
            }
            return "";
        }
        public string IsModifyDate(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'ModifyDate'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyDate = DateTime.Now;\r\n";
            }
            return "";
        }
        public string IsModifyUserId(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'ModifyUserId'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyUserId = OperatorProvider.Provider.Current().UserId;\r\n";
            }
            return "";
        }
        public string IsModifyUserName(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'ModifyUserName'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyUserName = OperatorProvider.Provider.Current().UserName;\r\n";
            }
            return "";
        }
        #endregion

        #region 实体映射类
        /// <summary>
        /// 生成实体映射类
        /// </summary>
        /// <param name="baseConfigModel">基本信息</param>
        /// <returns></returns>
        public string EntityMapBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Microsoft.EntityFrameworkCore;\r\n\r\n");
            sb.Append("using Microsoft.EntityFrameworkCore.Metadata.Builders;\r\n\r\n");

            sb.Append("namespace Application.Mapping." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 1.6\r\n");
            sb.Append("    /// Copyright (c) 2022-" + DateTime.Now.Year + " \r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "-实体映射类\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.MapClassName + " : IEntityTypeConfiguration<" + baseConfigModel.EntityClassName + ">\r\n");
            sb.Append("    {\r\n");
            sb.Append("        public  void Configure" + $"(EntityTypeBuilder<{baseConfigModel.EntityClassName}> builder)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            #region 表、主键\r\n");
            sb.Append("            //表\r\n");
            sb.Append("            builder.ToTable(\"" + baseConfigModel.DataBaseTableName + "\");\r\n");
            sb.Append("            //主键\r\n");
            sb.Append("            builder.HasKey(t => t." + baseConfigModel.DataBaseTablePK + ");\r\n");
            sb.Append("            #endregion\r\n\r\n");

            sb.Append("            #region 配置关系\r\n");
            sb.Append("            #endregion\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 服务类
        /// <summary>
        /// 生成服务类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string ServiceBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Application.IService." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Data.Repository.Molde;\r\n");
            sb.Append("using Util;\r\n");
            sb.Append("using Util.Extension;\r\n");
            sb.Append("using SqlSugar;\r\n");
            sb.Append("using System.Linq.Expressions;\r\n\r\n");

            sb.Append("namespace Application.Service." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 1.6\r\n");
            sb.Append("    /// Copyright (c) 2022-" + DateTime.Now.Year + " \r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "-服务类\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.ServiceClassName + " : BaseService<" + baseConfigModel.EntityClassName + ">, " + baseConfigModel.IServiceClassName + "\r\n");
            sb.Append("    {\r\n");

            sb.Append("        #region 获取数据\r\n");
            //  if (baseConfigModel.gridModel.IsPage == true)
            // {
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("           Expressionable<" + baseConfigModel.EntityClassName + "> expressionable = Expressionable.Create<" + baseConfigModel.EntityClassName + ">();\r\n\r\n");
            sb.Append("           dynamic queryParam = queryJson.ToJObject();\r\n");
            sb.Append("           //查询条件\r\n");
            sb.Append("          //string keyword = queryParam.keyword;\r\n");
            sb.Append("          //expressionable.AndIF(!keyword.IsEmpty(), c => c.Name.Contains(keyword));\r\n\r\n");

            sb.Append("          Expression<Func<" + baseConfigModel.EntityClassName + ", bool>> expression = expressionable.ToExpression();\r\n\r\n");
            sb.Append("          return this.BaseRepository().FindList(expression, pagination);\r\n");
            sb.Append("        }\r\n");
            // }
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("           Expressionable<" + baseConfigModel.EntityClassName + "> expressionable = Expressionable.Create<" + baseConfigModel.EntityClassName + ">();\r\n\r\n");
            sb.Append("           dynamic queryParam = queryJson.ToJObject();\r\n");
            sb.Append("           //查询条件\r\n");
            sb.Append("          //string keyword = queryParam.keyword;\r\n");
            sb.Append("          //expressionable.AndIF(!keyword.IsEmpty(), c => c.Name.Contains(keyword));\r\n\r\n");

            sb.Append("           Expression<Func<" + baseConfigModel.EntityClassName + ", bool>> expression = expressionable.ToExpression();\r\n\r\n");
            sb.Append("           return this.BaseRepository().FindList(expression).ToList();\r\n");
            sb.Append("         }\r\n");
            sb.Append("        #endregion\r\n\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 服务接口类
        /// <summary>
        /// 生成服务接口类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string IServiceBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Data.Repository.Molde;\r\n");
            sb.Append("namespace Application.IService." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 1.6\r\n");
            sb.Append("    /// Copyright (c) 2022-" + DateTime.Now.Year + " \r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "-服务接口类\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public interface " + baseConfigModel.IServiceClassName + " : IBaseService<" + baseConfigModel.EntityClassName + ">\r\n");
            sb.Append("    {\r\n");
            sb.Append("        #region 获取数据\r\n");
            // if (baseConfigModel.gridModel.IsPage == true)
            // {
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取分页列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson);\r\n");
            // }
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson);\r\n");

            sb.Append("        #endregion\r\n\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 业务类
        /// <summary>
        /// 生成业务类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string BusinesBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Application.IService." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Application.Service." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Data.Repository.Molde;\r\n");
            sb.Append("namespace Application.Busines." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 1.6\r\n");
            sb.Append("    /// Copyright (c) 2022-" + DateTime.Now.Year + " \r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "-业务类\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.BusinesClassName + " : BaseBLL<" + baseConfigModel.EntityClassName + ">\r\n");
            sb.Append("    {\r\n");
            sb.Append("        private " + baseConfigModel.IServiceClassName + " service = new " + baseConfigModel.ServiceClassName + "();\r\n\r\n");

            sb.Append("        #region 获取数据\r\n");
            //  if (baseConfigModel.gridModel.IsPage == true)
            //{
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取分页列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetPageList(pagination, queryJson);\r\n");
            sb.Append("        }\r\n");
            // }
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetList(queryJson);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");

            return sb.ToString();
        }
        #endregion

        #region 控制器
        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string ControllerBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Application.Busines." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Util;\r\n");
            sb.Append("using Util.Extension;\r\n");
            sb.Append("using Data.Repository.Molde;\r\n");
            sb.Append("using Microsoft.AspNetCore.Mvc;\r\n");
            sb.Append("using System.Linq;\r\n");
            sb.Append("using Application.Code;\r\n");
            sb.Append("using Application.Web.Controllers;\r\n\r\n");
            sb.Append("namespace Application.Web.Areas." + baseConfigModel.OutputAreas + ".Controllers\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 1.6\r\n");
            sb.Append("    /// Copyright (c) 2022-" + DateTime.Now.Year + " \r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "-控制器\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    [Area(\"" + baseConfigModel.OutputAreas + "\")]\r\n");
            sb.Append("    [ControllerNmae(Name = \"" + baseConfigModel.Description + "\")]\r\n");
            sb.Append("    public class " + baseConfigModel.ControllerName + " : MvcControllerBase\r\n");
            sb.Append("    {\r\n");
            sb.Append("        private " + baseConfigModel.BusinesClassName + " " + baseConfigModel.BusinesClassName.ToLower() + " = new " + baseConfigModel.BusinesClassName + "();\r\n\r\n");

            sb.Append("        #region 视图功能\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 列表页面\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        [ControllerNmae(Name = \"列表视图\")]\r\n");
            sb.Append("        public ActionResult " + baseConfigModel.IndexPageName + "()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return View();\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 表单页面\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        [ControllerNmae(Name = \"表单视图\")]\r\n");
            sb.Append("        public ActionResult " + baseConfigModel.FormPageName + "()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return View();\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 获取数据\r\n");
            // if (baseConfigModel.gridModel.IsPage == true)
            //  {
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取分页列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表Json</returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        [ControllerNmae(Name = \"获取分页列表\")]\r\n");
            sb.Append("        public ActionResult GetPageListJson(Pagination pagination, string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            var watch = CommonHelper.TimerStart();\r\n");
            sb.Append("            var data = " + baseConfigModel.BusinesClassName.ToLower() + ".GetPageList(pagination, queryJson);\r\n");
            sb.Append("             var jsonData = new                \r\n");
            sb.Append("             {                                 \r\n");
            sb.Append("                 code = 0,                     \r\n");
            sb.Append("                 data = data,                  \r\n");
            sb.Append("                 count = pagination.records,     \r\n");
            sb.Append("                 msg = \"\"                      \r\n");
            sb.Append("             };                                \r\n");
            sb.Append("            return ToJsonResult(jsonData);\r\n");
            sb.Append("        }\r\n");
            //   }
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表Json</returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        [ControllerNmae(Name = \"获取列表\")]\r\n");
            sb.Append("        public ActionResult GetListJson(string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            var data = " + baseConfigModel.BusinesClassName.ToLower() + ".GetList(queryJson);\r\n");
            sb.Append("            return ToJsonResult(data);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体 \r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns>返回对象Json</returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        [ControllerNmae(Name = \"获取表单\")]\r\n");
            sb.Append("        public ActionResult GetFormJson(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            var data = " + baseConfigModel.BusinesClassName.ToLower() + ".GetEntity(keyValue);\r\n");
            sb.Append("            return ToJsonResult(data);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [AjaxOnly]\r\n");
            sb.Append("        [ControllerNmae(Name = \"删除\")]\r\n");
            sb.Append("        [HandlerAuthorize(PermissionMode.Enforce)]\r\n");
            sb.Append("        public ActionResult RemoveForm(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            " + baseConfigModel.BusinesClassName.ToLower() + ".RemoveForm(keyValue);\r\n");
            sb.Append("            return Success(\"删除成功。\");\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单(新增、修改)\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [AjaxOnly]\r\n");
            sb.Append("        [ControllerNmae(Name = \"新增/修改\")]\r\n");
            sb.Append("        [HandlerAuthorize(PermissionMode.Enforce)]\r\n");
            sb.Append("        public ActionResult SaveForm(string keyValue, " + baseConfigModel.EntityClassName + " entity)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            " + baseConfigModel.BusinesClassName.ToLower() + ".SaveForm(keyValue, entity,true);\r\n");
            sb.Append("            return Success(\"操作成功。\");\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region WebAPI控制器
        /// <summary>
        /// 生成WebAPI控制器
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string WebAPIControllerBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Application.Busines." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using Util;\r\n");
            sb.Append("using Util.Extension;\r\n");
            sb.Append("using Microsoft.AspNetCore.Authorization;\r\n");
            sb.Append("using Microsoft.AspNetCore.Mvc;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Linq;\r\n");
            sb.Append("using AutoMapper;\r\n");
            sb.Append("using Util.WebControl;\r\n");
            sb.Append("using System;\r\n");

            sb.Append("namespace Application.WebAPI.Controllers\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "-WebAPI控制器\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.ControllerName + " : APIControllerBase\r\n");
            sb.Append("    {\r\n");
            sb.Append("        private " + baseConfigModel.BusinesClassName + " " + baseConfigModel.BusinesClassName.ToLower() + " = new " + baseConfigModel.BusinesClassName + "();\r\n\r\n");

            sb.Append(" private readonly IMapper _mapper;         \r\n");
            sb.Append(" /// <summary>                             \r\n");
            sb.Append(" /// 初始化 依赖注入                        \r\n");
            sb.Append(" /// </summary>                            \r\n");
            sb.Append(" /// <param name=\"mapper\"></param>         \r\n");
            sb.Append(" public " + baseConfigModel.ControllerName + "(IMapper mapper)     \r\n");
            sb.Append(" {                                         \r\n");
            sb.Append("     _mapper = mapper;                     \r\n");
            sb.Append(" }                                         \r\n");

            sb.Append("        #region 获取数据\r\n");
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("       /// <summary>                                                                                                                       \r\n");
                sb.Append("       ///  查询" + baseConfigModel.Description + "分页列表                                                                                                                \r\n");
                sb.Append("       /// </summary>                                                                                                                      \r\n");
                sb.Append("       /// <param name=\"page\">当前页码</param>                                                                                             \r\n");
                sb.Append("       /// <param name=\"rows\">每页行数</param>                                                                                             \r\n");
                sb.Append("       /// <param name=\"sidx\">排序字段</param>                                                                                             \r\n");
                sb.Append("       /// <param name=\"sord\">排序类型(desc,asc)</param>                                                                                   \r\n");
                sb.Append("       /// <param name=\"queryJson\">查询条件</param>                                                                                        \r\n");
                sb.Append("       /// <returns></returns>                                                                                                             \r\n");
                sb.Append("       [Route(\"api/" + baseConfigModel.ControllerName.Replace("Controller", "") + "/GetPageList\")]                                                                                                     \r\n");
                sb.Append("       [HttpGet]                                                                                                                           \r\n");
                sb.Append("      [ProducesResponseType(200, Type = typeof(List< " + baseConfigModel.EntityClassName + ">))]     \r\n");
                sb.Append("       public IActionResult GetPageList(int page, int rows, string sidx = null, string sord = null, string queryJson = null)         \r\n");
                sb.Append("       {                                                                                                                                   \r\n");
                sb.Append("           Pagination pagination = new Pagination()                                                                                        \r\n");
                sb.Append("           {                                                                                                                               \r\n");
                sb.Append("           page = page,                                                                                                                    \r\n");
                sb.Append("           rows = rows,                                                                                                                    \r\n");
                sb.Append("           sidx = sidx == null ? \"CreateDate\" : sidx,                                                                                      \r\n");
                sb.Append("           sord = sord == null ? \"desc\" : sord,                                                                                            \r\n");
                sb.Append("           records = 0                                                                                                                     \r\n");
                sb.Append("           };                                                                                                                              \r\n");
                sb.Append("           var watch = CommonHelper.TimerStart();                                                                                          \r\n");
                sb.Append("           var data = " + baseConfigModel.BusinesClassName.ToLower() + ".GetPageList(pagination, queryJson);                                \r\n");
                sb.Append("           var jsonData = new                                                                                                              \r\n");
                sb.Append("           {                                                                                                                               \r\n");
                sb.Append("           rows = data,                                                                                                                    \r\n");
                sb.Append("           total = pagination.total,                                                                                                       \r\n");
                sb.Append("           page = pagination.page,                                                                                                         \r\n");
                sb.Append("           records = pagination.records,                                                                                                   \r\n");
                sb.Append("           costtime = CommonHelper.TimerEnd(watch)                                                                                         \r\n");
                sb.Append("           };                                                                                                                              \r\n");
                sb.Append("           return Success(\"获取成功\", jsonData);                                                                                           \r\n");
                sb.Append("       }                                                                                                                                   \r\n");

            }
            sb.Append("      /// <summary>                                                               \r\n");
            sb.Append("      /// 获取所有用户列表                                                         \r\n");
            sb.Append("      /// </summary>                                                              \r\n");
            sb.Append("      /// <returns></returns>                                                     \r\n");
            sb.Append("      [Route(\"api/" + baseConfigModel.ControllerName.Replace("Controller", "") + "/GetList\")]                                               \r\n");
            sb.Append("      [HttpGet]                                                                   \r\n");
            sb.Append("      [ProducesResponseType(200, Type = typeof(List< " + baseConfigModel.EntityClassName + ">))]     \r\n");
            sb.Append("      public IActionResult GetList()                                        \r\n");
            sb.Append("      {                                                                           \r\n");
            sb.Append("          try                                                                     \r\n");
            sb.Append("          {                                                                       \r\n");
            sb.Append("              var data = " + baseConfigModel.BusinesClassName.ToLower() + ".GetList(\"{}\");                                 \r\n");
            sb.Append("              return Success(\"获取成功\", data);                                   \r\n");
            sb.Append("          }                                                                       \r\n");
            sb.Append("          catch (Exception ex)                                                    \r\n");
            sb.Append("          {                                                                       \r\n");
            sb.Append("              return Error(ex.Message);                                           \r\n");
            sb.Append("          }                                                                       \r\n");
            sb.Append("      } \r\n");

            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [ValidateAntiForgeryToken]\r\n");
            sb.Append("        [AjaxOnly]\r\n");
            sb.Append("        public IActionResult RemoveForm(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            " + baseConfigModel.BusinesClassName.ToLower() + ".RemoveForm(keyValue);\r\n");
            sb.Append("            return Success(\"删除成功。\");\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单(新增、修改)\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [ValidateAntiForgeryToken]\r\n");
            sb.Append("        [AjaxOnly]\r\n");
            sb.Append("        public IActionResult SaveForm(string keyValue, " + baseConfigModel.EntityClassName + " entity)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            " + baseConfigModel.BusinesClassName.ToLower() + ".SaveForm(keyValue, entity);\r\n");
            sb.Append("            return Success(\"操作成功。\");\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 列表页
        /// <summary>
        /// 表头显示/隐藏
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string IsShow_Field(bool field)
        {
            if (field == true)
            {
                return ",hidden: true";
            }
            return "";
        }
        /// <summary>
        /// 生成列表页
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string IndexBuilder(BaseConfigModel baseConfigModel)
        {


            var areasUrl = baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10);
            StringBuilder sb = new StringBuilder();
            StringBuilder tool = new StringBuilder();
            StringBuilder js = new StringBuilder();

            sb.Append("@{\r\n");
            sb.Append("    ViewBag.Title = \"" + baseConfigModel.Description + "-列表页面\";\r\n");
            sb.Append("    Layout = \"~/Views/Shared/_Index.cshtml\";\r\n");
            sb.Append("}\r\n");
            sb.Append("<script>;\r\n");
            sb.Append("    $(function () {\r\n");
            sb.Append("        Gettable();\r\n");
            sb.Append("    });\r\n");
            sb.Append("    //加载表格\r\n");
            sb.Append("    function Gettable() {\r\n");
            sb.Append("       var queryJson = {};\r\n");

            sb.Append("    var wherejson = {                        \r\n");
            sb.Append("    sidx: '" + baseConfigModel.DataBaseTablePK + "',                          \r\n");
            sb.Append("    sord: 'desc'                             \r\n");
            sb.Append("    }                                        \r\n");


            sb.Append("  $('#gridTable').layTable({      \r\n ");
            sb.Append("   even: true, height: 'full',    \r\n ");
            sb.Append("   postData: JSON.stringify(queryJson), \r\n ");
            sb.Append("   title: '" + baseConfigModel.Description + "',\r\n");
            sb.Append("   toolbar: 'no', \r\n");
            sb.Append("   limit: 30,  \r\n");
            sb.Append("   page: true,  \r\n");
            sb.Append("   where: wherejson,\r\n ");
            sb.Append("   cols: [[\r\n ");
            List<GridColumnModel> colModel = baseConfigModel.gridColumnModel;
            if (colModel != null)
            {
                foreach (GridColumnModel entity in colModel)
                {
                    sb.Append("      { field: '" + entity.field + "', title: '" + entity.title + "', align: '" + entity.align + "', minWidth: " + entity.minWidth + ",sort:" + entity.sort.ToString().ToLower());

                    switch (entity.tp)
                    {
                        case "1":  // 编辑框
                            sb.Append(",templet: \"#" + entity.field + "InputTpl\" ");
                            tool.Append("<script type=\"text/html\" id=\"" + entity.field + "InputTpl\">\r\n ");
                            tool.Append("     <input type=\"text\"  data-action-blur=\"/" + areasUrl + "/SaveForm\" data-value=\"keyValue#{{d." + baseConfigModel.DataBaseTablePK + "}};" + entity.field + "#{value}\" data-loading=\"false\" value=\"{{d." + entity.field + "}}\" class=\"layui-input text-center\">\r\n ");
                            tool.Append(" </script>\r\n");
                            break;
                        case "2":  // 数字框
                            sb.Append(",templet: \"#" + entity.field + "InputTpl\" ");
                            tool.Append("<script type=\"text/html\" id=\"" + entity.field + "InputTpl\">\r\n ");
                            tool.Append("     <input type=\"number\" min=\"0\" data-blur-number=\"0\" data-action-blur=\"/" + areasUrl + "/SaveForm\" data-value=\"keyValue#{{d." + baseConfigModel.DataBaseTablePK + "}};" + entity.field + "#{value}\" data-loading=\"false\" value=\"{{d." + entity.field + "}}\" class=\"layui-input text-center\">\r\n ");
                            tool.Append(" </script>\r\n");
                            break;
                        case "3":  // 开关
                            sb.Append(",templet: \"#" + entity.field + "EnabledMark\" ");
                            tool.Append(" <script type=\"text/html\" id=\"" + entity.field + "EnabledMark\">\r\n");
                            tool.Append("      <input type=\"checkbox\" value=\"{{d." + baseConfigModel.DataBaseTablePK + "}}\" lay-skin=\"switch\" lay-text=\"启用|停用\" lay-filter=\"" + entity.field + "EnabledMark\" data-debug=\"{{d." + entity.field + "}}\" {{d." + entity.field + ">0 ?'checked':''}}>     \r\n");
                            tool.Append("  </script>\r\n");
                            js.Append("  $.form.SetSwitch({\r\n");
                            js.Append("       Id:\"" + entity.field + "\",\r\n");
                            js.Append("       Switch:\"" + entity.field + "EnabledMark\",\r\n");
                            js.Append("       url:\"/" + areasUrl + "/SaveForm\",\r\n");
                            js.Append("  })\r\n");
                            break;
                        default:
                            break;
                    }

                    sb.Append(" }, \r\n");

                }
            }
            sb.Append("      { toolbar: '#toolbar', title: '操作面板', align: 'center', minWidth: 160, fixed: 'right' }\r\n ");
            sb.Append("            ]] \r\n");
            sb.Append("       ,done: function(res, curr, count) {   \r\n");
            sb.Append("          // authorizeButton($('body')); //按钮权限      \r\n");
            sb.Append("       }                                     \r\n");
            sb.Append("       });\r\n");

            sb.Append("   //$('#btn_Search').click(function() {                    \r\n");
            sb.Append("  //    queryJson = {                                       \r\n");
            sb.Append("   //   keyword: $(\"#txt_Keyword\").val()                  \r\n");
            sb.Append("    //  }                                                   \r\n");
            sb.Append("    //  wherejson.queryJson = JSON.stringify(queryJson);    \r\n");
            sb.Append("      //执行重载                                          \r\n");
            sb.Append("    //  layui.table.reload('gridTable', {                   \r\n");
            sb.Append("    //  where: wherejson,                                   \r\n");
            sb.Append("    //  page: { curr: 1 }                                   \r\n");
            sb.Append("    //  });                                                 \r\n");
            sb.Append(" // });                                                     \r\n");

            sb.Append("               }                                           \r\n");
            sb.Append("</script>\r\n");

            sb.Append("    <div class=\"layui-card\">                                                                                                                                                                                                                                                                  \r\n");
            sb.Append("      <div class=\"layui-card-header\"><span class=\"layui-icon font-s10 color-desc margin-right-5\">&#xe65b;</span>" + baseConfigModel.Description + "                                                                                                                                                                    \r\n");
            sb.Append("        <div class=\"pull-right toolbar\">                                                                                                                                                                                                                                                      \r\n");
            sb.Append("                                                                                                                                                                                                                                                                                              \r\n");
            sb.Append("            <div class=\"btn-group\">                                                                                                                                                                                                                                                           \r\n");
            sb.Append("                <a  class=\"btn lr-add layui-btn layui-btn-primary layui-btn-sm\" data-modal=\"/" + areasUrl + "/" + baseConfigModel.FormPageName + "\"><i class=\"fa fa-plus\"></i>&nbsp;新增</a>                                                                                                                         \r\n");
            // sb.Append("                <a  class=\"lr-delete layui-btn layui-btn-primary layui-btn-sm\" data-action='/" + areasUrl + "/RemoveAllForm' data-table-id=\"gridTable\" data-rule=\"keyValue#{" + baseConfigModel.DataBaseTablePK + "}\" data-confirm=\"确定要批量删除吗？\"<i class=\"fa fa-trash-o\"></i>&nbsp;批量删除</a>    \r\n");
            sb.Append("            </div>                                                                                                                                                                                                                                                                            \r\n");
            sb.Append("            <script>//authorizeButton($('body'))</script>                                                                                                                                                                                                                                   \r\n");
            sb.Append("        </div>                                                                                                                                                                                                                                                                                \r\n");
            sb.Append("      </div>                                                                                                                                                                                                                                                                                  \r\n");
            sb.Append("      <div class=\"layui-card-line\"></div>                                                                                                                                                                                                                                                     \r\n");
            sb.Append("      <div class=\"layui-card-body\">                                                                                                                                                                                                                                                           \r\n");
            sb.Append("                                                                                                                                                                                                                                                                                              \r\n");
            sb.Append("        <div class=\"layui-card-table\">                                                                                                                                                                                                                                                        \r\n");
            sb.Append("            <div class=\"layui-tab layui-tab-card\">                                                                                                                                                                                                                                            \r\n");
            sb.Append("             <div class=\"layui-tab-content\">                                                                                                                                                                                                                                                  \r\n");
            sb.Append("                <form action=\"#\" autocomplete=\"off\" class=\"layui-form layui-form-pane form-search\" method=\"get\" onsubmit=\"return false\" data-table-id=\"gridTable\"><!--  -->                                                                                                \r\n");
            sb.Append("                   <div class=\"layui-form-item layui-inline\">                                                                                                                                                                                                                                 \r\n");
            sb.Append("                       <label class=\"layui-form-label\">关键词</label>                                                                                                                                                                                                                         \r\n");
            sb.Append("                       <label class=\"layui-input-inline\">                                                                                                                                                                                                                                     \r\n");
            sb.Append("                           <input class=\"layui-input\" id=\"keyword\" name=\"keyword\" placeholder=\"请输入关键词\" value=\"\">                                                                                                                                                                       \r\n");
            sb.Append("                        </label>                                                                                                                                                                                                                                                              \r\n");
            sb.Append("                   </div>                                                                                                                                                                                                                                                                     \r\n");
            sb.Append("                    <div class=\"layui-form-item layui-inline\">                                                                                                                                                                                                                                \r\n");
            sb.Append("                        <button class=\"layui-btn layui-btn-primary\" id=\"btn_Search\"> <i class=\"layui-icon\"></i> 搜 索</button>                                                                                                                                                              \r\n");
            sb.Append("                    </div>                                                                                                                                                                                                                                                                    \r\n");
            sb.Append("                 </form>                                                                                                                                                                                                                                                                      \r\n");
            sb.Append("                                                                                                                                                                                                                                                                                              \r\n");
            sb.Append("                 <table id=\"gridTable\" data-url=\"/" + areasUrl + "/GetPageListJson\"  data-id=\"gridTable\" lay-filter=\"gridTable\"></table>                                                                                                        \r\n");
            sb.Append("             </div>\r\n");
            sb.Append("            </div>\r\n");
            sb.Append("         </div>                                                                                                                                                                                                                                                                               \r\n");
            sb.Append("    </div>\r\n");
            sb.Append("    </div> \r\n");


            sb.Append("  <script type = \"text/html\" id = \"toolbar\" >\r\n");
            sb.Append("      <a class=\"btn lr-edit layui-btn layui-btn-primary layui-btn-sm\" data-modal='/" + areasUrl + "/" + baseConfigModel.FormPageName + "?keyValue={{d." + baseConfigModel.DataBaseTablePK + "}}'>编 辑</a>      \r\n");
            sb.Append("      <a class=\"btn lr-del layui-btn layui-btn-primary layui-btn-sm\" data-del='/" + areasUrl + "/RemoveForm' data-id=\"{{d." + baseConfigModel.DataBaseTablePK + "}}\">删除</a>   \r\n");
            sb.Append("  </script>\r\n");

            string jsstr = js.ToString();
            if (jsstr != "")
            {
                jsstr = "<script>" + jsstr + "</script>";
            }
            return sb.ToString() + "\r\n" + tool.ToString() + "\r\n" + jsstr;

        }

        /// <summary>
        /// 预览列表页
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string IndexLooker(BaseConfigModel baseConfigModel)
        {


            var areasUrl = baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10);
            StringBuilder sb = new StringBuilder();
            StringBuilder tool = new StringBuilder();
            StringBuilder js = new StringBuilder();

            string json = string.Join(',', baseConfigModel.gridColumnModel.Select(t => t.field).ToList());
            sb.Append("<script>\r\n");
            sb.Append("    $(function () {\r\n");
            sb.Append("        Gettable();\r\n");
            sb.Append("    });\r\n");
            sb.Append("    //加载表格\r\n");
            sb.Append("    function Gettable() {\r\n");
            sb.Append("       var queryJson = {};\r\n");

            sb.Append("    var wherejson = {                        \r\n");
            sb.Append("    sidx: '" + baseConfigModel.DataBaseTablePK + "',\r\n");
            sb.Append("    sord: 'desc'                             \r\n");
            sb.Append("    }                                        \r\n");


            sb.Append("  $('#gridTable').layTable({      \r\n ");
            sb.Append("   even: true, height: 'full',    \r\n ");
            sb.Append("   postData: queryJson, \r\n ");
            sb.Append("   data:[], \r\n ");
            sb.Append("   title: '" + baseConfigModel.Description + "',\r\n");
            sb.Append("   toolbar: 'no', \r\n");
            sb.Append("   limit: 30,  \r\n");
            sb.Append("   page: true,  \r\n");
            sb.Append("   where: wherejson,\r\n ");
            sb.Append("   cols: [[\r\n ");
            List<GridColumnModel> colModel = baseConfigModel.gridColumnModel;
            if (colModel != null)
            {
                foreach (GridColumnModel entity in colModel)
                {
                    sb.Append("      { field: '" + entity.field + "', title: '" + entity.title + "', align: '" + entity.align + "', minWidth: " + entity.minWidth + ",sort:" + entity.sort.ToString().ToLower());

                    switch (entity.tp)
                    {
                        case "1":  // 编辑框
                            sb.Append(",templet: \"#" + entity.field + "InputTpl\" ");
                            tool.Append("<script type=\"text/html\" id=\"" + entity.field + "InputTpl\">\r\n ");
                            tool.Append("     <input type=\"text\"  data-action-blur=\"/" + areasUrl + "/SaveForm\" data-value=\"keyValue#{{d." + baseConfigModel.DataBaseTablePK + "}};" + entity.field + "#{value}\" data-loading=\"false\" value=\"{{d." + entity.field + "}}\" class=\"layui-input text-center\">\r\n ");
                            tool.Append(" </script>\r\n");
                            break;
                        case "2":  // 数字框
                            sb.Append(",templet: \"#" + entity.field + "InputTpl\" ");
                            tool.Append("<script type=\"text/html\" id=\"" + entity.field + "InputTpl\">\r\n ");
                            tool.Append("     <input type=\"number\" min=\"0\" data-blur-number=\"0\" data-action-blur=\"/" + areasUrl + "/SaveForm\" data-value=\"keyValue#{{d." + baseConfigModel.DataBaseTablePK + "}};" + entity.field + "#{value}\" data-loading=\"false\" value=\"{{d." + entity.field + "}}\" class=\"layui-input text-center\">\r\n ");
                            tool.Append(" </script>\r\n");
                            break;
                        case "3":  // 开关
                            sb.Append(",templet: \"#" + entity.field + "EnabledMark\" ");
                            tool.Append(" <script type=\"text/html\" id=\"" + entity.field + "EnabledMark\">\r\n");
                            tool.Append("      <input type=\"checkbox\" value=\"{{d." + baseConfigModel.DataBaseTablePK + "}}\" lay-skin=\"switch\" lay-text=\"启用|停用\" lay-filter=\"" + entity.field + "EnabledMark\" data-debug=\"{{d." + entity.field + "}}\" {{d." + entity.field + ">0 ?'checked':''}}>     \r\n");
                            tool.Append("  </script>\r\n");
                            js.Append(" <script>\r\n $.form.SetSwitch({\r\n");
                            js.Append("       Id:\"" + entity.field + "\",\r\n");
                            js.Append("       Switch:\"" + entity.field + "EnabledMark\",\r\n");
                            js.Append("       url:\"/" + areasUrl + "/SaveForm\",\r\n");
                            js.Append("  })\r\n</script>\r\n");
                            break;
                        default:
                            break;
                    }

                    sb.Append(" }, \r\n");

                }
            }
            sb.Append("      { toolbar: '#toolbar', title: '操作面板', align: 'center', minWidth: 160, fixed: 'right' }\r\n ");
            sb.Append("            ]] \r\n");
            sb.Append("       ,done: function(res, curr, count) {   \r\n");
            sb.Append("          // authorizeButton($('body')); //按钮权限      \r\n");
            sb.Append("       }                                     \r\n");
            sb.Append("       });\r\n");

            sb.Append("   //$('#btn_Search').click(function() {                    \r\n");
            sb.Append("  //    queryJson = {                                       \r\n");
            sb.Append("   //   keyword: $(\"#txt_Keyword\").val()                  \r\n");
            sb.Append("    //  }                                                   \r\n");
            sb.Append("    //  wherejson.queryJson = JSON.stringify(queryJson);    \r\n");
            sb.Append("      //执行重载                                          \r\n");
            sb.Append("    //  layui.table.reload('gridTable', {                   \r\n");
            sb.Append("    //  where: wherejson,                                   \r\n");
            sb.Append("    //  page: { curr: 1 }                                   \r\n");
            sb.Append("    //  });                                                 \r\n");
            sb.Append(" // });                                                     \r\n");

            sb.Append("               }                                           \r\n");
            sb.Append("</script>\r\n");

            sb.Append("    <div class=\"layui-card\">                                                                                                                                                                                                                                                                  \r\n");
            sb.Append("      <div class=\"layui-card-header\"><span class=\"layui-icon font-s10 color-desc margin-right-5\">&#xe65b;</span>" + baseConfigModel.Description + "                                                                                                                                                                    \r\n");
            sb.Append("        <div class=\"pull-right toolbar\">                                                                                                                                                                                                                                                      \r\n");
            sb.Append("                                                                                                                                                                                                                                                                                              \r\n");
            sb.Append("            <div class=\"btn-group\">                                                                                                                                                                                                                                                           \r\n");
            sb.Append("                <a  class=\"btn lr-add layui-btn layui-btn-primary layui-btn-sm\" data-modal=\"/" + areasUrl + "/" + baseConfigModel.FormPageName + "\"><i class=\"fa fa-plus\"></i>&nbsp;新增</a>                                                                                                                         \r\n");
            // sb.Append("                <a  class=\"lr-delete layui-btn layui-btn-primary layui-btn-sm\" data-action='/" + areasUrl + "/RemoveAllForm' data-table-id=\"gridTable\" data-rule=\"keyValue#{" + baseConfigModel.DataBaseTablePK + "}\" data-confirm=\"确定要批量删除吗？\"<i class=\"fa fa-trash-o\"></i>&nbsp;批量删除</a>    \r\n");
            sb.Append("            </div>                                                                                                                                                                                                                                                                            \r\n");
            sb.Append("            <script>//authorizeButton($('body'))</script>                                                                                                                                                                                                                                   \r\n");
            sb.Append("        </div>                                                                                                                                                                                                                                                                                \r\n");
            sb.Append("      </div>                                                                                                                                                                                                                                                                                  \r\n");
            sb.Append("      <div class=\"layui-card-line\"></div>                                                                                                                                                                                                                                                     \r\n");
            sb.Append("      <div class=\"layui-card-body\">                                                                                                                                                                                                                                                           \r\n");
            sb.Append("                                                                                                                                                                                                                                                                                              \r\n");
            sb.Append("        <div class=\"layui-card-table\">                                                                                                                                                                                                                                                        \r\n");
            sb.Append("            <div class=\"layui-tab layui-tab-card\">                                                                                                                                                                                                                                            \r\n");
            sb.Append("             <div class=\"layui-tab-content\">                                                                                                                                                                                                                                                  \r\n");
            sb.Append("                <form action=\"#\" autocomplete=\"off\" class=\"layui-form layui-form-pane form-search\" method=\"get\" onsubmit=\"return false\" data-table-id=\"gridTable\"><!--  -->                                                                                                \r\n");
            sb.Append("                   <div class=\"layui-form-item layui-inline\">                                                                                                                                                                                                                                 \r\n");
            sb.Append("                       <label class=\"layui-form-label\">关键词</label>                                                                                                                                                                                                                         \r\n");
            sb.Append("                       <label class=\"layui-input-inline\">                                                                                                                                                                                                                                     \r\n");
            sb.Append("                           <input class=\"layui-input\" id=\"txt_Keyword\" name=\"name\" placeholder=\"请输入关键词\" value=\"\">                                                                                                                                                                       \r\n");
            sb.Append("                        </label>                                                                                                                                                                                                                                                              \r\n");
            sb.Append("                   </div>                                                                                                                                                                                                                                                                     \r\n");
            sb.Append("                    <div class=\"layui-form-item layui-inline\">                                                                                                                                                                                                                                \r\n");
            sb.Append("                        <button class=\"layui-btn layui-btn-primary\" id=\"btn_Search\"> <i class=\"layui-icon\"></i> 搜 索</button>                                                                                                                                                              \r\n");
            sb.Append("                    </div>                                                                                                                                                                                                                                                                    \r\n");
            sb.Append("                 </form>                                                                                                                                                                                                                                                                      \r\n");
            sb.Append("                                                                                                                                                                                                                                                                                              \r\n");

            sb.Append("                 <table id=\"gridTable\" data-url=\"/GeneratorManage/SingleTable/LookListTestPageList?gridColumnJson=" + json + "\"  data-id=\"gridTable\" lay-filter=\"gridTable\"></table>                                                                                                        \r\n");
            sb.Append("             </div>\r\n");
            sb.Append("            </div>\r\n");
            sb.Append("         </div>                                                                                                                                                                                                                                                                               \r\n");
            sb.Append("    </div>\r\n");
            sb.Append("    </div> \r\n");


            sb.Append("  <script type = \"text/html\" id = \"toolbar\" >\r\n");
            sb.Append("      <a class=\"btn lr-edit layui-btn layui-btn-primary layui-btn-sm\" data-modal='/" + areasUrl + "/" + baseConfigModel.FormPageName + "?keyValue={{d." + baseConfigModel.DataBaseTablePK + "}}'>编 辑</a>      \r\n");
            sb.Append("      <a class=\"btn lr-del layui-btn layui-btn-primary layui-btn-sm\" data-del='/" + areasUrl + "/RemoveForm' data-id=\"{{d." + baseConfigModel.DataBaseTablePK + "}}\">删除</a>   \r\n");
            sb.Append("  </script>\r\n");

            return sb.ToString() + "\r\n" + tool.ToString() + "\r\n" + js.ToString();

        }

        #endregion

        #region 表单页
        /// <summary>
        /// 生成表单页
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string FormBuilder(BaseConfigModel baseConfigModel)
        {
            var areasUrl = baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10);
            StringBuilder sb = new StringBuilder();





            sb.Append("@{\r\n");
            sb.Append("    ViewBag.Title = \"" + baseConfigModel.Description + " - 表单页面\";\r\n");
            sb.Append("    Layout = \"~/Views/Shared/_Form.cshtml\";\r\n");
            sb.Append("  if (!string.IsNullOrWhiteSpace(Context.Request.Query[\"keyValue\"]))      \r\n");
            sb.Append("  {                                                                       \r\n");
            sb.Append("      ViewBag.keyValue = Context.Request.Query[\"keyValue\"];               \r\n");
            sb.Append("  }                                                                       \r\n");
            sb.Append("}\r\n");

            sb.Append("<script>\r\n");
            sb.Append("    var keyValue = \"@ViewBag.keyValue\";\r\n");
            sb.Append("    $(function () {\r\n");
            sb.Append("                initControl();                        \r\n");
            sb.Append("    });\r\n");




            sb.Append("    //初始化控件\r\n");
            sb.Append("    function initControl() {\r\n");
            sb.Append("        //获取表单\r\n");
            sb.Append("        if (!!keyValue) {\r\n");
            sb.Append("            $.form.SetForm({\r\n");
            sb.Append("                url: \"../../" + areasUrl + "/GetFormJson\",\r\n");
            sb.Append("                param: { keyValue: keyValue },\r\n");
            sb.Append("                success: function (data) {\r\n");
            sb.Append("                $.form.SetWebControls($('#form1'), data);\r\n");



            sb.Append("                }\r\n");
            sb.Append("            })\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");

            sb.Append("    //保存表单;\r\n");
            sb.Append("    function AcceptClick() {\r\n");
            sb.Append("  if (!Validform($('#form1')))\r\n");
            sb.Append("  {                           \r\n");
            sb.Append("      return false;           \r\n");
            sb.Append("  }                           \r\n");
            sb.Append("      var postData = $('#form1').serializeArray();\r\n");

            sb.Append("         $.form.SaveForm({\r\n");
            sb.Append("            url: \"../../" + areasUrl + "/SaveForm?keyValue=\" + keyValue,\r\n");
            sb.Append("            param: postData,\r\n");
            sb.Append("            loading: \"正在保存数据...\",\r\n");
            sb.Append("            success: function () {\r\n");
            sb.Append("                   $.form.reload();  $.msg.myclose();\r\n");
            sb.Append("            }\r\n");
            sb.Append("        })\r\n");
            sb.Append("    }\r\n");
            sb.Append("</script>\r\n");
            sb.Append(" <style>\r\n");
            sb.Append("  #layfrom .layui-input-inline{min-width:500px; }\r\n");
            sb.Append(" </style>\r\n");
            sb.Append("  <div style=\"padding: 15px; max-height: 700px; overflow-y: scroll;\" class=\"layui-form-pane\" id=\"layfrom\">\r\n");

            List<FormFieldModel> fieldModel = baseConfigModel.formFieldModel;
            if (fieldModel != null)
            {
                int colnum = 1;//第几个控件
                               // int tcolnum = 1;//当前第几行
                int coljs = 0;//行计数  一行=12 大于12+1
                foreach (FormFieldModel entity in fieldModel)
                {
                    int clumnIndex = entity.ControlColspan == null ? 12 : (int)entity.ControlColspan;//每行中第几列
                    string col = "";

                    if (clumnIndex <= 10)  // 占位少于10/12 则执行多行计算 至少留1/12 作为间隔 1/12 作为容器
                    {
                        if (coljs == 0)
                        {
                            col = "<div class=\"layui-col-md" + clumnIndex + " \">";   //当前行第一个控件 不设置间距
                            coljs += clumnIndex; //计算是否下一行
                        }
                        else
                        {
                            coljs += clumnIndex; //计算是否下一行
                            coljs++; //增加了 1间距
                            if (coljs >= 12)
                            {
                                col = "<div class=\"layui-col-md" + clumnIndex + " \">"; //当前控件已经超出 当行范围 添加到下一行
                            }
                            else
                            {
                                col += "<div class=\"layui-col-md" + clumnIndex + " layui-col-md-offset1 \">";  //不是当前行第一个控件 设置1间距
                            }
                        }
                        if (coljs > 10) //占位大于10 / 12 则计算到下一行  至少留1/ 12 作为间隔 1 / 12 作为容器
                        {
                            coljs = 0;  //重置计数

                        }

                    }
                    else
                    {

                        coljs = 0;  //重置行计数
                    }


                    sb.Append("  " + col + "\r\n");
                    sb.Append("  <div  class=\"layui-form-item\" >\r\n");
                    sb.Append("    <label class=\"layui-form-label layui-form-required\" style=\"width: 110px;\">" + entity.title + "</label>\r\n");
                    sb.Append("    <div class=\"layui-input-block\" style=\"margin-left: 110px\">\r\n");
                    sb.Append("      " + CreateControl(entity) + "\r\n");
                    sb.Append("    </div>\r\n");
                    sb.Append("  </div>\r\n");
                    if (col != "")
                    {
                        sb.Append("  </div>\r\n");
                    }
                    colnum++;
                }
            }
            sb.Append(" </div>\r\n");
            sb.Append("    <div style=\"border-top: 1px solid #eee; height: 50px; line-height: 50px; text-align: center; padding: 0px;\">             \r\n");
            sb.Append("          <input  class=\"layui-btn  layui-btn-normal\"  onclick=\"AcceptClick()\" type=\"button\" value=\"确认提交\"/>    \r\n");
            sb.Append("  </div>                                                                                                                     \r\n");


            return sb.ToString();
        }


        /// <summary>
        /// 生成表单页
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string FormLooker(BaseConfigModel baseConfigModel)
        {
            var areasUrl = baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10);
            StringBuilder sb = new StringBuilder();

            sb.Append(" <script>$(function() {;\r\n");
            sb.Append("layui.form.render();\r\n");
            sb.Append("});</script>\r\n");


            sb.Append(" <style>\r\n");
            sb.Append("  #layfrom .layui-input-inline{min-width:500px; }\r\n");
            sb.Append(" </style>\r\n");
            sb.Append("  <div style=\"padding: 15px; height: 700px; overflow-y: scroll;\" class=\"layui-form-pane layui-form\" id=\"layfrom\">\r\n");

            List<FormFieldModel> fieldModel = baseConfigModel.formFieldModel;
            if (fieldModel != null)
            {
                int colnum = 1;//第几个控件
                               // int tcolnum = 1;//当前第几行
                int coljs = 0;//行计数  一行=12 大于12+1
                foreach (FormFieldModel entity in fieldModel)
                {
                    int clumnIndex = entity.ControlColspan == null ? 12 : (int)entity.ControlColspan;//每行中第几列
                    string col = "";

                    if (clumnIndex <= 10)  // 占位少于10/12 则执行多行计算 至少留1/12 作为间隔 1/12 作为容器
                    {
                        if (coljs == 0)
                        {
                            col = "<div class=\"layui-col-md" + clumnIndex + " \">";   //当前行第一个控件 不设置间距
                            coljs += clumnIndex; //计算是否下一行
                        }
                        else
                        {
                            coljs += clumnIndex; //计算是否下一行
                            coljs++; //增加了 1间距
                            if (coljs >= 12)
                            {
                                col = "<div class=\"layui-col-md" + clumnIndex + " \">"; //当前控件已经超出 当行范围 添加到下一行
                            }
                            else
                            {
                                col += "<div class=\"layui-col-md" + clumnIndex + " layui-col-md-offset1 \">";  //不是当前行第一个控件 设置1间距
                            }
                        }
                        if (coljs > 10) //占位大于10 / 12 则计算到下一行  至少留1/ 12 作为间隔 1 / 12 作为容器
                        {
                            coljs = 0;  //重置计数

                        }

                    }
                    else
                    {

                        coljs = 0;  //重置行计数
                    }


                    sb.Append("  " + col + "\r\n");
                    sb.Append("  <div  class=\"layui-form-item\" >\r\n");
                    sb.Append("    <label class=\"layui-form-label layui-form-required\" style=\"width: 110px;\">" + entity.title + "</label>\r\n");
                    sb.Append("    <div class=\"layui-input-block\" style=\"margin-left: 110px\">\r\n");
                    sb.Append("      " + CreateControl(entity) + "\r\n");
                    sb.Append("    </div>\r\n");
                    sb.Append("  </div>\r\n");
                    if (col != "")
                    {
                        sb.Append("  </div>\r\n");
                    }
                    colnum++;
                }
            }
            sb.Append(" </div>\r\n");


            return sb.ToString();
        }
        /// <summary>
        /// 生成控件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CreateControl(FormFieldModel entity)
        {
            StringBuilder sbControl = new StringBuilder();
            string ControlName = entity.title;                            //属性名称
            string ControlId = entity.field;                                //控件Id
            string ControlType = entity.tp;                            //控件类型
            string validator_html = "";
            if (entity.Validator == "1")
            {
                validator_html = "required isvalid=\"yes\" checkexpession=\"" + entity.Validatorstr + "\"";
            }
            switch (ControlType)
            {
                case "0"://文本框
                    sbControl.Append("<input id=\"" + ControlId + "\" type=\"text\" class=\" layui-input\" placeholder=\"请输入" + ControlName + "\"  name=\"" + ControlId + "\" " + validator_html + " />");
                    break;
                case "2"://文件上传
                    sbControl.Append("   <input class=\"layui-input think-bg-gray\" name=\"" + ControlId + "\" type=\"hidden\" id=\"" + ControlId + "\" placeholder=\"请上传头像\" readonly=readonly value='' />\r\n");
                    sbControl.Append("  <div class=\"layui-btn layui-btn-primary layui-btn-sm\" data-Myfile data-accept=\".gif,.png,.jpg,.jpeg\" data-out=\"" + ControlId + "\" data-more=\"false\" data-add=\"true\">上传文件</div>");
                    break;
                case "3"://下拉框
                    sbControl.Append("<select id=\"" + ControlId + "\" name=\"" + ControlId + "\"  type=\"select\" class=\"layui-select\" " + validator_html + "></select>");
                    break;
                case "1"://开关
                    sbControl.Append("<script>\r\nlayui.form.on('switch(" + ControlId + "ch)', function(data) {   \r\n ");
                    sbControl.Append("    if (this.checked) {                                   \r\n ");
                    sbControl.Append("    $(\"#" + ControlId + "cs\").val(\"1\");                         \r\n ");
                    sbControl.Append("        } else                                            \r\n ");
                    sbControl.Append("        {                                                 \r\n ");
                    sbControl.Append("    $(\"#" + ControlId + "cs\").val(\"0\")                          \r\n ");
                    sbControl.Append("        }                                                 \r\n ");
                    sbControl.Append("        });                                               \r\n </script>\r\n");
                    sbControl.Append("    <input name=\"" + ControlId + "\" type=\"hidden\"  value=\"1\" id=\"" + ControlId + "cs\" >  \r\n  ");
                    sbControl.Append("    <input lay-skin=\"switch\" lay-text=\"启用|停用\" name=\"" + ControlId + "ch\" type=\"checkbox\"  value=\"0\" id=\"" + ControlId + "ch\" lay-filter=\"" + ControlId + "ch\" > ");
                    break;
                case "4"://日期框
                    sbControl.Append("<input id=\"" + ControlId + "\" type=\"text\" name=\"" + ControlId + "\"  data-date-input=\"datetime\"  class=\"layui-input\"  " + validator_html + "/>");
                    break;
                case "5"://多行文本框
                    sbControl.Append("<textarea id=\"" + ControlId + "\" name=\"" + ControlId + "\" class=\"layui-textarea\" " + validator_html + "></textarea>");
                    break;
                case "6"://编辑器
                    sbControl.Append("<script>\r\n require(['ckeditor'], function() {\r\n");
                    sbControl.Append("     window.createEditor('[name=" + ControlId + "]', { height: 250})\r\n");
                    sbControl.Append("  });\r\n</script>\r\n");
                    sbControl.Append("<textarea id=\"" + ControlId + "\" name=\"" + ControlId + "\" class=\"layui-textarea\" " + validator_html + "></textarea>");
                    break;
                default:
                    return "内部错误，配置有错误";
            }
            return sbControl.ToString();

        }
        #endregion
    }
}
