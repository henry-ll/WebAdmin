using KnifeUI.Swagger.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.Framework.Filters;

namespace WebAdmin.Framework.Extentions.Exceptions
{
    /// <summary>
    /// Swagger接口文档生成
    /// </summary>
    public static class SwaggerDocExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="nameList"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, List<string> nameList)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("basesystem", new OpenApiInfo
                {
                    Title = "公用接口",
                    Version = "v1",
                    Description = "",
                    Contact = new OpenApiContact { Name = "WebAdmin", Email = "" },
                });
                c.SwaggerDoc("test", new OpenApiInfo
                {
                    Title = "Test接口",
                    Version = "v1",
                    Description = "",
                    Contact = new OpenApiContact { Name = "WebAdmin", Email = "" },
                });
                c.AddServer(new OpenApiServer()
                {
                    Url = "",
                    Description = ""
                });
                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.OperationFilter<HttpHeaderOperationFilter>();
                c.DocumentFilter<HttpHeaderDocmentFilter>();//隐藏具体Api
                var security = new Dictionary<string, IEnumerable<string>> { { "", Array.Empty<string>() } };
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT授权token前面需要加上字段Bearer与一个空格,如Bearer token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                // 使用反射获取xml文件。并构造出文件的路径
                nameList.ForEach(f =>
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, f+ ".xml");
                    // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                    c.IncludeXmlComments(xmlPath, true);
                });
            });
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            // 启用Swagger中间件
            app.UseSwagger();

            // 配置SwaggerUI
            app.UseKnife4UI(c =>
            {
                c.RoutePrefix = "";
                c.DocumentTitle = "WebAPI";
                c.SwaggerEndpoint($"basesystem/api-docs", $"公用接口");
                c.SwaggerEndpoint($"test/api-docs", $"Test接口");
            });
            return app;
        }
    }
}
