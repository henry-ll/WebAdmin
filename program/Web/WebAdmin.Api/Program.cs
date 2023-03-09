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

#region Services ����
void ConfigureServices()
{
    //��ʼ������
    builder.Services.InitConfiguration(Directory.GetCurrentDirectory(), Configuration);
    //��ʼ��Serilog��־
    builder.Host.UseSerilog();
    //ע��SeriLog
    builder.Services.AddSingleton(Log.Logger);
    //�������ı�html����
    builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
    builder.Services.AddHttpContextAccessor();
    //����ȫ�ֻ�ȡHttpContext
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //����ϵͳ����
    builder.Services.Configure<AppSystem>(Configuration.GetSection(nameof(AppSystem)));
    ConfigServiceProvider.Init(builder.Services);
    AppHttpContext.Configure(builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>());
    //���ö����̬�ļ��з���Ȩ��
    builder.Services.AddManyStaticFiles(builder, Configuration);
    builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    //�ӿ��ĵ�
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
    //MemoryCache����
    builder.Services.AddMemoryCache();
    //���󻺴�
    builder.Services.AddResponseCaching();
    //������ؽ��ʱ���ı�����
    builder.Services.AddControllersWithViews().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });
    //ȫ���쳣������
    builder.Services.AddControllersWithViews(option => { option.Filters.Add(typeof(GlobalApiExceptionFilter)); });
    //���ÿ���
    builder.Services.AddApiCors(builder);
    builder.Services.AddAutoMapperProfile();//autoMapper �����Զ�ӳ��

    Assembly[] array = new Assembly[]
    {
        Assembly.Load("WebAdmin.Infrastructure"),
        Assembly.Load("WebAdmin.Service"),
        Assembly.Load("WebAdmin.Repositories"),
    };
    //�Զ�ע��
    builder.Services.AutoInjection(AutoInjectionExtention.InjectionType.scope, array);
    //������ؽ��ʱ���ı�����
    builder.Services.AddControllersWithViews().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });
}
#endregion

#region app ����
void Configure()
{
#if DEBUG
        //Swagger�����ĵ������¼
        app.UseSwaggerAuthorized();
        //ʹ��UseKnife4UI����Swagger�ӿ��ĵ�
        app.UseSwaggerDoc();
        app.MapSwagger("{documentName}/api-docs");
#else
#endif

    app.UseHttpsRedirection();
    //����Cors
    app.UseCors();
    //��֤
    app.UseAuthentication();
    //��Ȩ
    app.UseAuthorization();
    app.MapControllers();
    app.UseRouting();
    //Ĭ���ļ��м�� wwwroot Index.htm Index.html default.htm default.html
    app.UseDefaultFiles();
    //У��Get/Post��������м��
    app.UseParamsMiddleware();
    //���ö����̬�ļ��з���
    app.UseManyStaticFiles(Configuration);
}
#endregion
