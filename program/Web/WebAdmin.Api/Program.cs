using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using SqlSugar.DistributedSystem.Snowflake;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebAdmin.Framework.Exceptions;
using WebAdmin.Framework.Extentions.Exceptions;
using WebAdmin.Framework.Formatters;
using WebAdmin.Framework.Util;
using WebAdmin.Framework.Providers;
using WebAdmin.Framework.Middleware;
using ServiceStack;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsystem.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", true, reloadOnChange: true)
    .AddCommandLine(args)
    .AddEnvironmentVariables().Build();

ConfigureServices();

var app = builder.Build();

Configure();

app.Run();


try
{
    Log.Information("starting web Host!");
}
catch (System.Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly!");
}
finally
{
    Log.CloseAndFlush();
}

#region Services 配置
void ConfigureServices()
{
    //初始化配置
    builder.Services.InitConfiguration(Directory.GetCurrentDirectory(), Configuration);
    //初始化Serilog日志
    builder.Host.UseSerilog();
    //注入SeriLog
    builder.Services.AddSingleton(Log.Logger);
    //处理中文被html编码
    builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
    builder.Services.AddHttpContextAccessor();
    //配置全局获取HttpContext
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //配置系统配置
    builder.Services.Configure<AppSystem>(Configuration.GetSection(nameof(AppSystem)));
    ConfigServiceProvider.Init(builder.Services);
    AppHttpContext.Configure(builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>());
    //配置多个静态文件夹访问权限
    builder.Services.AddManyStaticFiles(builder, Configuration);
    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    //接口文档
    var assemblies = Assembly.GetEntryAssembly()?.GetReferencedAssemblies();
    var assemblyNamelist = assemblies.Where(x => x.Name.Contains("WebAdmin")).Select(x => x.Name).ToList();
    assemblyNamelist.Add(Assembly.GetCallingAssembly().GetName().Name);
    builder.Services.AddSwaggerDoc(assemblyNamelist).AddControllers().AddJsonOptions(configure =>
    {
        configure.JsonSerializerOptions.PropertyNamingPolicy = null;
        configure.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
    }).ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
        options.ClientErrorMapping[404].Link = "https://*/404";
    });

    builder.Services.AddJwt(builder);
    //MemoryCache缓存
    builder.Services.AddMemoryCache();
    //请求缓存
    builder.Services.AddResponseCaching();
    //解决返回结果时中文被编码
    builder.Services.AddControllersWithViews().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });
    //全局异常过滤器
    builder.Services.AddControllersWithViews(option => { option.Filters.Add(typeof(GlobalApiExceptionFilter)); });
    //设置跨域
    builder.Services.AddApiCors(builder);
    builder.Services.AddAutoMapperProfile();//autoMapper 对象自动映射

    Assembly[] array = new Assembly[]
    {
        Assembly.Load("WebAdmin.Infrastructure"),
        Assembly.Load("WebAdmin.Service"),
        Assembly.Load("WebAdmin.Repositories"),
    };
    //自动注入
    builder.Services.AutoInjection(AutoInjectionExtention.InjectionType.scope, array);
    //解决返回结果时中文被编码
    builder.Services.AddControllersWithViews().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });
}
#endregion

#region app 配置
void Configure()
{
#if DEBUG
        //Swagger在线文档密码登录
        app.UseSwaggerAuthorized();
        //使用UseKnife4UI美化Swagger接口文档
        app.UseSwaggerDoc();
        app.MapSwagger("{documentName}/api-docs");
#else
#endif

    app.UseHttpsRedirection();
    //配置Cors
    app.UseCors();
    //认证
    app.UseAuthentication();
    //授权
    app.UseAuthorization();
    app.MapControllers();
    app.UseRouting();
    //默认文件中间件 wwwroot Index.htm Index.html default.htm default.html
    app.UseDefaultFiles();
    //校验Get/Post传入参数中间件
    app.UseParamsMiddleware();
    //启用多个静态文件夹访问
    app.UseManyStaticFiles(Configuration);
}
#endregion
