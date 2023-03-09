using CodeGenerator;
using WebAdmin.Framework.Extentions;
using WebAdmin.Framework.Helper;

namespace CodeGenerator
{
    /// <summary>
    /// 自动创建代码
    /// </summary>
    public class CreateCodeFile
    {
        /// <summary>
        /// 执行创建文件
        /// </summary>
        /// <param name="baseConfigModel">基本信息</param>
        /// <param name="strCode">生成代码内容</param>
        public static void CreateExecution(BaseConfigModel baseConfigModel, string strCode)
        {
            var strParam = strCode.ToJsonStringConvertJObject();

            #region 实体类（WebAdmin.Entity）
            string entityCode = strParam["entityCode"].ToString();
            string entityPath = baseConfigModel.OutputEntity + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.EntityClassName + ".cs";
            if (!System.IO.File.Exists(entityPath))
            {
                DirFileHelper.CreateFileContent(entityPath, entityCode);
            }
            #endregion

            #region 业务类（WebAdmin.Repositories）
            string businesCode = strParam["businesCode"].ToString();
            string businesPath = baseConfigModel.OutputBusines + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.BusinesClassName + ".cs";
            if (!System.IO.File.Exists(businesPath))
            {
                DirFileHelper.CreateFileContent(businesPath, businesCode);
            }
            #endregion

            #region 接口类（WebAdmin.Infrastructure）
            string iserviceCode = strParam["iserviceCode"].ToString();
            string iservicePath = baseConfigModel.OutputIService + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.IServiceClassName + ".cs";
            if (!System.IO.File.Exists(iservicePath))
            {
                DirFileHelper.CreateFileContent(iservicePath, iserviceCode);
            }
            #endregion

            #region 服务类（WebAdmin.Service）
            string serviceCode = strParam["serviceCode"].ToString();
            string servicePath = baseConfigModel.OutputService + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.ServiceClassName + ".cs";
            if (!System.IO.File.Exists(servicePath))
            {
                DirFileHelper.CreateFileContent(servicePath, serviceCode);
            }
            #endregion

            #region WebApi控制器（WebAdmin.Api）
            string controllerCode = strParam["controllerCode"].ToString();
            string controllerPath = baseConfigModel.OutputController + "\\Areas\\" + baseConfigModel.OutputAreas + "\\Controllers\\" + baseConfigModel.ControllerName + ".cs";
            if (!System.IO.File.Exists(controllerPath))
            {
                DirFileHelper.CreateFileContent(controllerPath, controllerCode);
            }
            #endregion

            #region Mvc控制器（WebAdmin.Api）
            //string controllerCode = strParam["controllerCode"].ToString();
            //string controllerPath = baseConfigModel.OutputController + "\\Areas\\" + baseConfigModel.OutputAreas + "\\Controllers\\" + baseConfigModel.ControllerName + ".cs";
            //if (!System.IO.File.Exists(controllerPath))
            //{
            //    DirFileHelper.CreateFileContent(controllerPath, controllerCode);
            //}
            #endregion
        }
    }
}
